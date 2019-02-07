using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VM
{
    public partial class Form1 : Form
    {
        private List<Coin> coinsVM = new List<Coin>();
        private List<Coin> coinsCust = new List<Coin>();
        private List<Product> products = new List<Product>();
        private int sum;
     
        public Form1()
        {
            InitializeComponent();
            sum = 0;
            coinsCust.AddRange(new List<Coin>() { new Coin(1, 10), new Coin(2, 30), new Coin(5, 20), new Coin(10, 15) });
            coinsVM.AddRange(new List<Coin>() { new Coin(1, 100), new Coin(2, 100), new Coin(5, 100), new Coin(10, 100) });
            products.AddRange(new List<Product> { new Product("Чай", 13, 10), new Product("Кофе", 18, 20), new Product("Кофе с молоком", 21, 20), new Product("Сок", 35, 15) });
            CustomerofPurse( 10, 30);
            VMPurse(400, 30);
            MoneyDisplay(200, 5);
            ProductsDisplay(10, 200); 
        }

        private void CustomerofPurse(int left, int top)
        {
            Label title = new Label()
            {
                Location = new Point(left * 2, top / 6),
                Text = "Кошелек пользователя",
                AutoSize = true
            };
            Controls.Add(title);

            int topLabel = top + 5;
            foreach (var x in coinsCust)
            {
                int coinValue = x.coinValue;
                Button button = new Button
                {
                    Left = left,
                    Top = top,
                    Name = "btnСoin" + coinValue,
                    Text = coinValue + " руб.",
                    Tag = coinValue
                };
                button.Click += ButtonOnClick;
                Controls.Add(button);
                top += button.Height + 2;

                Label label = new Label();
                label.Name = "labelCoinQuantity" + x.coinValue;
                label.Text = x.quantity + " шт.";
                label.Location = new Point(left + 100, topLabel);
                label.AutoSize = true;
                this.Controls.Add(label);
                topLabel += label.Height + 12;
            }
        }

        private void ButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int coin = (int)button.Tag;
            int index = coinsCust.FindIndex(x => x.coinValue == coin);
            int quantity = coinsCust[index].quantity;
            if (quantity != 0)
            {
                quantity--;
                sum += coin;
                coinsCust[index].quantity = quantity;
                coinsVM[coinsVM.FindIndex(x => x.coinValue == coin)].quantity += 1;
                (Controls["labelCoinQuantity" + coin] as Label).Text = quantity + " шт.";
                (Controls["dataGridView"] as DataGridView).Refresh();
                (Controls["lblsum"] as Label).Text = sum.ToString();
            }
        }

        private void VMPurse(int left, int top)
        {
            Label title = new Label()
            {
                Location = new Point(left + 50, top / 6),
                Text = "Кошелек VM"
            };
            Controls.Add(title);
            DataGridView dt = new DataGridView()
            {
                Name = "dataGridView",
                DataSource = coinsVM,
                Location = new Point(left, top),
                BorderStyle = BorderStyle.None,
                BackgroundColor = SystemColors.Control,
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.None,
                RowHeadersVisible = false,
                AutoSize = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Enabled = false
            };
            Controls.Add(dt);
        }

        private void MoneyDisplay(int left, int top)
        {
            Label label = new Label()
            {
                Location = new Point(left, top),
                Text = "Внесенная сумма:",
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            Controls.Add(label);
            Label lblsum = new Label()
            {
                Name = "lblsum",
                Location = new Point(left + 50, top + 35),
                Text = sum.ToString(),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            Controls.Add(lblsum);
            Button btnchange = new Button()
            {
                Name = "btnchange",
                Text = "Сдача",
                Location = new Point(left + 25, top + 65),
            };
            btnchange.Click += Btnchange_Click;
            Controls.Add(btnchange);
        }

        private void Btnchange_Click(object sender, EventArgs e)
        {
            Change(sum);
            sum = 0;
        }

        private void Change(int sum)
        {
            bool moneybox = false;
            for (int a = 0; 1 * a <= sum; a++)
            {
                for (int b = 0; 2 * b <= sum; b++)
                {
                    for (int c = 0; 5 * c <= sum; c++)
                    {
                        for (int d = 0; 10 * d <= sum; d++)
                        {
                            if (1 * a + 2 * b + 5 * c + 10 * d == sum)
                            {
                                foreach (var x in coinsVM)
                                {
                                    switch (x.coinValue)
                                    {
                                        case 1:
                                            x.quantity -= a;
                                            break;
                                        case 2:
                                            x.quantity -= b;
                                            break;
                                        case 5:
                                            x.quantity -= c;
                                            break;
                                        case 10:
                                            x.quantity -= d;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                foreach (var x in coinsCust)
                                {
                                    switch (x.coinValue)
                                    {
                                        case 1:
                                            x.quantity += a;
                                            break;
                                        case 2:
                                            x.quantity += b;
                                            break;
                                        case 5:
                                            x.quantity += c;
                                            break;
                                        case 10:
                                            x.quantity += d;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                
                                moneybox = true;
                                break;
                            }
                        }
                        if (moneybox)
                        {
                            break;
                        }
                    }
                    if (moneybox)
                    {
                        break;
                    }
                }
                if (moneybox)
                {
                    break;
                }
            }
            (Controls["dataGridView"] as DataGridView).Refresh();
            (Controls["lblsum"] as Label).Text = "0";
            foreach (var x in coinsCust)
            {
                (Controls["labelCoinQuantity" + x.coinValue] as Label).Text = x.quantity + " шт.";
            }
        }

        private void ProductsDisplay(int left, int top)
        {
            DataGridView dt = new DataGridView()
            {
                Name = "dgvProductDisplay",
                DataSource = products,
                Location = new Point(left, top),
                BorderStyle = BorderStyle.None,
                BackgroundColor = SystemColors.Control,
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.None,
                RowHeadersVisible = false,
                AutoSize = true,
            };
            dt.CellContentClick += Dt_CellContentClick;
            Controls.Add(dt);
            DataGridViewButtonColumn btnChooseProduct = new DataGridViewButtonColumn()
            {                 
                Text = "Выбрать",
                UseColumnTextForButtonValue = true,   
            };
            (Controls["dgvProductDisplay"] as DataGridView).Columns.Add(btnChooseProduct);
        }

        private void Dt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (products[e.RowIndex].quantity != 0)
            {
                if (sum > products[e.RowIndex].cost)
                {
                    sum -= products[e.RowIndex].cost;
                    (Controls["lblsum"] as Label).Text = sum.ToString();
                    products[e.RowIndex].quantity -= 1;
                    (Controls["dgvProductDisplay"] as DataGridView).Refresh();
                    MessageBox.Show("Спасибо!");
                }
                else
                    MessageBox.Show("Недостаточно средств");
            }
        }        
    }
}
