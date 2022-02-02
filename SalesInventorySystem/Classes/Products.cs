using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem
{
    class Products
    {
        String prodcode, prodname;
        double produnitprice,prodsellingprice, proddiscount, prodcharge;
        public Products()
        {

        }

        public void setProductCode(string pcode)
        {
            this.prodcode = pcode;
        }

        public void setProductName(string pname)
        {
            this.prodname = pname;
        }

        public void setProductUnitPrice(double punitprice)
        {
            this.produnitprice = punitprice;
        }

        public void setProductSellingPrice(double psellingprice)
        {
            this.prodsellingprice = psellingprice;
        }

        public void setProductDiscount(double pdiscount)
        {
            this.proddiscount = pdiscount;
        }

        public void setProductCharge(double pcharge)
        {
            this.prodcharge = pcharge;
        }

        public String getProductCode()
        {
            return prodcode;
        }
        public String getProductName()
        {
            return prodname;
        }
        public Double getProductUnitPrice()
        {
            return produnitprice;
        }
        public Double getProductSellingPrice()
        {
            return prodsellingprice;
        }
        public Double getProductDiscount()
        {
            return proddiscount;
        }
        public Double getProductCharge()
        {
            return prodcharge;
        }
    }
}
