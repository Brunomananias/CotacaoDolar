using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CotacaoDolar
{
    public partial class FrmCotacao : Form
    {
        public FrmCotacao()
        {
            InitializeComponent();
        }

      
        private void btnPesquisar_Click(object sender, EventArgs e)
        {


            string strURL = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=0dda388a";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(strURL).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    Market market = JsonConvert.DeserializeObject<Market>(result);

                    lblCompra.Text = string.Format(CultureInfo.GetCultureInfo("pt-br"), "{0:C}", market.Currency.Buy);

                    lblVenda.Text = string.Format(CultureInfo.GetCultureInfo("pt-br"), "{0:C}", market.Currency.Sell);

                    lblVariacao.Text = string.Format(CultureInfo.GetCultureInfo("pt-br"), "{0:P}", market.Currency.Variation);
                }
                else
                {
                    lblCompra.Text = "";
                    lblVenda.Text = "";
                    lblVariacao.Text = "";
                }

            }


        }
    }
}
