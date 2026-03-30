using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace SalesInventorySystem.V5
{
    public partial class V5Pay : Form//DevExpress.XtraEditors.XtraForm
    {
        private Login parent;
        public string referenceId; 
        public double amount;
        public V5ResponseData receipt;
        public bool failed;
        public V5Pay(Login parent, string referenceId, double amount) //POS
        {
            this.parent = parent;
            this.referenceId = referenceId;
            this.amount = amount;
            InitializeComponent();
            //#InitializeWebView();
        }
        async public Task InitializeWebView()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.WebMessageReceived += WebView_WebMessageReceived;
            webView21.NavigationCompleted += WebView_NavigationCompleted;
            //
            /*
                var referenceId = "TndPY2NoMndtd1c2WWZPU2";
                var amount = 100;
               var form = new V5Pay();
               await form.InitializeWebView();
               await form.CreatePayment(referenceId, amount);
               await form.showDailog();
               form.receipt
            */
        }
        async public Task CreatePayment()
        {
            var responseText = await GetResponse(this.referenceId, this.amount);
            if (responseText != null)
            {
                var responseData = JsonConvert.DeserializeObject<ResponseData>(responseText);
                if (responseData != null)
                {
                    if (responseData.Status == "ok")
                    {
                        await webView21.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(responseData.Script);
                        webView21.CoreWebView2.Navigate(responseData.RedirectUrl);
                        return;
                    }
                    //responseData.Status = "error";
                }
            }
            this.FailPayment("failed api response");
            //failed/error process for api not response either no internet-connection or api-no-response
        }
        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string data = e.TryGetWebMessageAsString();
            var responseData = JsonConvert.DeserializeObject<V5ResponseData>(data);
            if (responseData != null)
            {
                this.receipt = responseData;
                //https://api-doc.v5pay.com/#/en-us/common/appendix?id=payin-order-status
                if (responseData.OrderStatus == 2) 
                {
                    //
                    //#MessageBox.Show($"Data from Web: {data}");
                    // POS.SucessPayment(reference, amount);
                    this.Hide();
                    parent.SucessPayment(this);
                    this.Dispose();
                    // successfull process include process save to database
                    //--code here
                    // this.hide();
                    // perform print-receipt
                    return;
                }
            }
            this.FailPayment("failed payment");
            //failed message
            //#MessageBox.Show($"Data from Web: {data}");
        }
        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                // The page loaded successfully!
            }
            else
            {
                var errorStatus = e.WebErrorStatus;
                if (e.WebErrorStatus == CoreWebView2WebErrorStatus.HostNameNotResolved | e.WebErrorStatus == CoreWebView2WebErrorStatus.Disconnected)
                {
                    //failed message
                    this.FailPayment("failed internet connection");
                }
            }
        }
        private void FailPayment(string message)
        {
            this.Hide();
            this.failed = true;
            parent.FailPayment(this, message);
            this.Dispose();
        }
        public async Task<string> GetResponse(string referenceId, double amount)
        {
            string result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://x-payment.uat-ph.com/app/v1/payment/create?referenceId={referenceId}&amount={amount}";
                    var response = await client.GetAsync(url);
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                //# Console.WriteLine($"Network error: {e.Message}");
            }
            catch (TaskCanceledException e)
            {
                //# Console.WriteLine("The request timed out.");
            }
            return result;
        }
        public class ResponseData
        {
            public string Status;
            public string RedirectUrl;
            public string Script;
        }
        public class V5ResponseData
        {
            [JsonProperty("code")]
            public string StatusCode;
            [JsonProperty("message")]
            public string Message;
            [JsonProperty("amount")]
            public double Amount;
            [JsonProperty("orderStatus")]
            public int OrderStatus;
            [JsonProperty("productName")]
            public string ProductName;
        }
        /*
{
    "code": "1000",
    "message": "success",
    "merchantNo": "M1998987761205510177",
    "language": "en-US",
    "senderReceiverName": "ITCS",
    "currency": "PHP",
    "amount": 100.00,
    "formatAmount": "100.00",
    "orderStatus": 2, // success
    "productType": "QRPH_GCASH",
    "productLogoUrl": "https://v5pay-public.s3.ap-southeast-1.amazonaws.com/uat/payin_product_logo/6626a39924164cb7992b5037a481a15e.jpg",
    "orderNo": "31e4f91ede515e7b997d2a95a77f56f1",
    "transId": "11101186154634706945",
    "waterNo": "12101186154649387009",
    "redirectUrl": "http://your.site/return_url",
    "createTime": "2026-03-24 17:04:08",
    "orderMode": 10,
    "isAlipayPlus": false,
    "reSelection": false,
    "expireStatus": 0,
    "expiresAt": 1774346647000,
    "businessType": 0,
    "productName": "Qrph Gcash",
    "discountMode": 0,
    "sysCountryCode": "PH"
}

{
    "code": "1000",
    "message": "success",
    "merchantNo": "M1998987761205510177",
    "language": "en-US",
    "senderReceiverName": "ITCS",
    "currency": "PHP",
    "amount": 100.00,
    "formatAmount": "100.00",
    "orderStatus": 3, // fail/expired
    "productType": "QRPH_GCASH",
    "productLogoUrl": "https://v5pay-public.s3.ap-southeast-1.amazonaws.com/uat/payin_product_logo/6626a39924164cb7992b5037a481a15e.jpg",
    "orderNo": "903d864d71412ad16013ec73556c0483",
    "transId": "11101186296314101761",
    "waterNo": "12101186296332976129",
    "redirectUrl": "http://your.site/return_url",
    "createTime": "2026-03-25 11:50:05",
    "failReason": "success",
    "orderMode": 10,
    "isAlipayPlus": false,
    "reSelection": false,
    "expireStatus": 0,
    "expiresAt": 1774414205000,
    "businessType": 0,
    "productName": "Qrph Gcash",
    "discountMode": 0,
    "sysCountryCode": "PH"
}
    */
    }
}