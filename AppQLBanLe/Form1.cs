using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AppQLBanLe
{
    public partial class Form1 : Form
    {
        private List<List<string>> transactions = new List<List<string>>();
        private List<AssociationRule> learnedRules = new List<AssociationRule>();

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lstCart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCart_KeyDown);
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var rawData = File.ReadAllLines(filePath).Skip(1); // Skip header

                var transactionDict = new Dictionary<string, List<string>>();

                foreach (var line in rawData)
                {
                    var parts = line.Split(',');
                    if (parts.Length >= 3)
                    {
                        string transactionId = parts[0].Trim();
                        string product = parts[2].Trim();

                        if (!transactionDict.ContainsKey(transactionId))
                            transactionDict[transactionId] = new List<string>();

                        transactionDict[transactionId].Add(product);
                    }
                }

                transactions = transactionDict.Values.ToList();
                MessageBox.Show($"Đã tải {transactions.Count} giao dịch thành công!", "Tải dữ liệu");
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (transactions.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu giao dịch!", "Lỗi");
                return;
            }

            learnedRules = MineAssociationRules(transactions, minConfidence: 0.5);  // Tạm hạ xuống 0.5 để dễ ra luật
            MessageBox.Show($"Đã khai phá {learnedRules.Count} luật!", "Huấn luyện xong");

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string item = txtInput.Text.Trim();

            // So sánh ignore-case
            bool exists = lstCart.Items.Cast<string>().Any(x => string.Equals(x, item, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(item) && !exists)
            {
                lstCart.Items.Add(item);
                txtInput.Clear();
            }
        }

        private void btnRecommend_Click(object sender, EventArgs e)
        {
            if (learnedRules.Count == 0)
            {
                MessageBox.Show("Chưa có luật kết hợp nào. Hãy huấn luyện trước!", "Lỗi");
                return;
            }

            var currentCart = lstCart.Items.Cast<string>().ToList();
            var recommendedItems = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var rule in learnedRules)
            {
                if (rule.Antecedent.All(item => currentCart.Any(cartItem => string.Equals(cartItem, item, StringComparison.OrdinalIgnoreCase))))
                {
                    foreach (var consequentItem in rule.Consequent)
                    {
                        if (!currentCart.Any(cartItem => string.Equals(cartItem, consequentItem, StringComparison.OrdinalIgnoreCase)))
                        {
                            recommendedItems.Add(consequentItem);
                        }
                    }
                }
            }

            lstOutput.Items.Clear();
            if (recommendedItems.Count == 0)
            {
                lstOutput.Items.Add("Không có gợi ý phù hợp.");
            }
            else
            {
                foreach (var item in recommendedItems)
                {
                    lstOutput.Items.Add(item);
                }
            }
        }

        private List<AssociationRule> MineAssociationRules(List<List<string>> transactions, double minConfidence = 0.5)
        {
            var rules = new List<AssociationRule>();
            var itemCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var pairCounts = new Dictionary<(string, string), int>();

            foreach (var transaction in transactions)
            {
                var uniqueItems = transaction.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

                foreach (var item in uniqueItems)
                {
                    if (!itemCounts.ContainsKey(item))
                        itemCounts[item] = 0;
                    itemCounts[item]++;
                }

                for (int i = 0; i < uniqueItems.Count; i++)
                {
                    for (int j = 0; j < uniqueItems.Count; j++)
                    {
                        if (i == j) continue;
                        var pair = (uniqueItems[i], uniqueItems[j]);
                        if (!pairCounts.ContainsKey(pair))
                            pairCounts[pair] = 0;
                        pairCounts[pair]++;
                    }
                }
            }

            foreach (var pair in pairCounts)
            {
                string A = pair.Key.Item1;
                string B = pair.Key.Item2;
                int countAB = pair.Value;
                int countA = itemCounts.ContainsKey(A) ? itemCounts[A] : 0;

                if (countA == 0) continue;

                double confidence = (double)countAB / countA;
                if (confidence >= minConfidence)
                {
                    rules.Add(new AssociationRule
                    {
                        Antecedent = new List<string> { A },
                        Consequent = new List<string> { B },
                        Confidence = confidence
                    });
                }
            }

            return rules.OrderByDescending(r => r.Confidence).ToList();
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            lstCart.Items.Clear();
        }

        private void lstCart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstCart.SelectedItem != null)
            {
                lstCart.Items.Remove(lstCart.SelectedItem);
            }
        }

    }

    public class AssociationRule
    {
        public List<string> Antecedent { get; set; }
        public List<string> Consequent { get; set; }
        public double Confidence { get; set; }
    }
}
