using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
 * 2. Create a Class called Products with Productid, Product Name, Price.
 * Accept 10 Products, sort them based on the price, and display the sorted Products
 */

namespace CodeChallenge_2
{
    class Products
    {
        //properties
        public string ProductId { set; get; }
        public string ProductName { set; get; }

        public double Price { set; get; }

        List<Products> Ten_Products = new List<Products>();
        public void Accept_Products()
        {
            for(int i=0;i<10;i++)
            {
                Products product = new Products();
                Console.WriteLine("For Product {0}",i+1);
                Console.Write("Enter Name of the product {0} : ",i+1);
                product.ProductName = Console.ReadLine();
                Console.Write("Enter ProductId of the product {0} : ", i + 1);
                product.ProductId = Console.ReadLine();
                Console.Write("Enter Price of the product {0} : ", i + 1);
                product.Price = Convert.ToDouble(Console.ReadLine());
                Ten_Products.Add(product);
            }
        }

        //i used Quick sort algo for Sorting
        public void Sorting()
        {
            int length = Ten_Products.Count - 1;
            QuickSort(Ten_Products, 0, length);
        }

        private void QuickSort(List<Products> Ten_Products, int left, int right)
        {
            if (left < right)
            {
                int pivotInd = Partition(Ten_Products, left, right);
                QuickSort(Ten_Products, left, pivotInd - 1);
                QuickSort(Ten_Products, pivotInd + 1, right);
            }
        }

        private int Partition(List<Products> Ten_Products, int left, int right)
        {
            double pivot = Ten_Products[right].Price;
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (Ten_Products[j].Price < pivot)
                {
                    i++;
                    Swap_Products(Ten_Products, i, j);
                }
            }

            Swap_Products(Ten_Products, i + 1, right);
            return i + 1;
        }

        private void Swap_Products(List<Products> Ten_Products, int i, int j)
        {
            Products temp = new Products();
            temp=Ten_Products[i];
            Ten_Products[i] = Ten_Products[j];
            Ten_Products[j] = temp;
        }


        public void Display_Products()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("The ProductId is {0},ProductName is {1},ProductPrice is {2} :",Ten_Products[i].ProductId, Ten_Products[i].ProductName, Ten_Products[i].Price);
            }
        }
    }
    class Sorted_Products
    {
        static void Main()
        {
            Products products = new Products();
            Console.WriteLine("................Enter Products..............\n");
            products.Accept_Products();
            //Sorting , i used technique of Quick Sorting
            products.Sorting();
            Console.WriteLine("................Displaying Products After Sorting on Price..............\n");
            products.Display_Products();
            Console.Read();
        }
    }
}
