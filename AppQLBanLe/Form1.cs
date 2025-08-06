using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace AppQLBanLe
{
    public partial class dgvRules : Form
    {
        private List<List<string>> transactions = new List<List<string>>();
        private List<AssociationRule> learnedRules = new List<AssociationRule>();

        public dgvRules()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lstCart.KeyDown += lstCart_KeyDown;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var transactionDict = new Dictionary<string, List<string>>();

                using (TextFieldParser parser = new TextFieldParser(openFileDialog.FileName, System.Text.Encoding.UTF8))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.HasFieldsEnclosedInQuotes = true;

                    parser.ReadLine(); // Bỏ dòng tiêu đề

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields != null && fields.Length >= 3)
                        {
                            string transactionId = fields[0].Trim();
                            string product = fields[2].Trim().ToLowerInvariant();

                            if (!transactionDict.ContainsKey(transactionId))
                                transactionDict[transactionId] = new List<string>();

                            transactionDict[transactionId].Add(product);
                        }
                    }
                }

                transactions = transactionDict.Values.ToList();
                MessageBox.Show($"Đã tải {transactions.Count} giao dịch thành công!", "Tải dữ liệu");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file CSV:\n{ex.Message}", "Lỗi");
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (transactions.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu giao dịch!", "Lỗi");
                return;
            }

            double minSupport = 0.01;
            double minConfidence = 0.5;

            learnedRules = MineAssociationRules(transactions, minSupport, minConfidence);
            MessageBox.Show($"Đã khai phá {learnedRules.Count} luật!", "Huấn luyện xong");

            ShowRulesInGrid(learnedRules);
        }

        private void ShowRulesInGrid(List<AssociationRule> rules)
        {
            var viewModels = rules
                .OrderBy(r => string.Join(", ", r.Antecedent.Concat(r.Consequent)).ToLowerInvariant())
                .Select(r => new AssociationRuleViewModel
                {
                    Antecedent = string.Join(", ", r.Antecedent),
                    Consequent = string.Join(", ", r.Consequent),
                    Support = r.Support.ToString("P2"),
                    Confidence = r.Confidence.ToString("P2")
                })
                .ToList();

            dataGridViewTrain.DataSource = null;
            dataGridViewTrain.DataSource = viewModels;
        }


        private List<AssociationRule> MineAssociationRules(List<List<string>> transactions, double minSupport, double minConfidence)
        {
            int totalTransactions = transactions.Count;

            Dictionary<string, int> itemCounts = new Dictionary<string, int>();
            Dictionary<(string, string), int> pairCounts = new Dictionary<(string, string), int>();

            foreach (var txn in transactions)
            {
                var normalized = txn
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => s.Trim().ToLowerInvariant())
                    .Distinct()
                    .ToList();

                foreach (var item in normalized)
                {
                    if (!itemCounts.ContainsKey(item))
                        itemCounts[item] = 0;
                    itemCounts[item]++;
                }

                for (int i = 0; i < normalized.Count; i++)
                {
                    for (int j = i + 1; j < normalized.Count; j++)
                    {
                        string a = normalized[i];
                        string b = normalized[j];

                        var pair = string.Compare(a, b) <= 0 ? (a, b) : (b, a);

                        if (!pairCounts.ContainsKey(pair))
                            pairCounts[pair] = 0;

                        pairCounts[pair]++;
                    }
                }
            }

            var rules = new List<AssociationRule>();

            foreach (var pair in pairCounts)
            {
                string A = pair.Key.Item1;
                string B = pair.Key.Item2;
                int countAB = pair.Value;

                double supportAB = (double)countAB / totalTransactions;

                if (itemCounts.TryGetValue(A, out int countA) && countA > 0)
                {
                    double confidence = (double)countAB / countA;
                    if (confidence >= minConfidence && supportAB >= minSupport)
                    {
                        rules.Add(new AssociationRule
                        {
                            Antecedent = new List<string> { A },
                            Consequent = new List<string> { B },
                            Support = supportAB,
                            Confidence = confidence
                        });
                    }
                }

                if (itemCounts.TryGetValue(B, out int countB) && countB > 0)
                {
                    double confidence = (double)countAB / countB;
                    if (confidence >= minConfidence && supportAB >= minSupport)
                    {
                        rules.Add(new AssociationRule
                        {
                            Antecedent = new List<string> { B },
                            Consequent = new List<string> { A },
                            Support = supportAB,
                            Confidence = confidence
                        });
                    }
                }
            }

            return rules.OrderByDescending(r => r.Confidence).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string item = txtInput.Text.Trim().ToLowerInvariant();

            if (!string.IsNullOrEmpty(item) &&
                !lstCart.Items.Cast<string>().Any(x => string.Equals(x, item, StringComparison.OrdinalIgnoreCase)))
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

            var currentCart = lstCart.Items.Cast<string>()
                .Select(i => i.Trim().ToLowerInvariant())
                .ToHashSet();

            var recommended = new HashSet<string>();

            foreach (var rule in learnedRules)
            {
                var antecedentSet = rule.Antecedent
                    .Select(i => i.Trim().ToLowerInvariant())
                    .ToHashSet();

                if (antecedentSet.IsSubsetOf(currentCart))
                {
                    foreach (var consequent in rule.Consequent)
                    {
                        string normalized = consequent.Trim().ToLowerInvariant();
                        if (!currentCart.Contains(normalized))
                            recommended.Add(consequent);
                    }
                }
            }

            lstOutput.Items.Clear();
            if (recommended.Count == 0)
                lstOutput.Items.Add("Không có gợi ý phù hợp.");
            else
                foreach (var item in recommended)
                    lstOutput.Items.Add(item);
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            lstCart.Items.Clear();
        }

        private void lstCart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstCart.SelectedItem != null)
                lstCart.Items.Remove(lstCart.SelectedItem);
        }
    }

    public class AssociationRule
    {
        public List<string> Antecedent { get; set; } = new List<string>();
        public List<string> Consequent { get; set; } = new List<string>();
        public double Support { get; set; }
        public double Confidence { get; set; }
    }

    public class AssociationRuleViewModel
    {
        public string Antecedent { get; set; }
        public string Consequent { get; set; }
        public string Support { get; set; }
        public string Confidence { get; set; }
    }
}
