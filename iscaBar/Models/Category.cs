using iscaBar.Model;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace iscaBar.Models
{
    [Table("Category")]
    public class Category:ModelBase
    {
        private int id;
        [PrimaryKey, AutoIncrement]
        public int Id { get { return id; } set { id = value;OnPropertyChanged(); } }
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }
        private string description;
        public string Description { get { return description; } set { description = value; OnPropertyChanged(); } }
        private List<Category> child_ids;
        [OneToMany]
        public List<Category> Child_ids { get { return child_ids; } set { child_ids = value; OnPropertyChanged(); } }
        private Category catFather;
        private int parent_id;
        [ForeignKey(typeof(Category))]
        public int Parent_id { get { return parent_id; } set { parent_id = value; OnPropertyChanged(); } }
        [ManyToOne]
        public Category CatFather { get { return catFather; } set { catFather = value; OnPropertyChanged(); } }

        private List<Product> products;
        [ManyToMany(typeof(CategoryProduct))]
        public List<Product> Products { get { return products; } set { products = value; OnPropertyChanged(); } }
        public Category()
        {
            Child_ids = new List<Category>();
            Products = new List<Product>();

        }
    }
}
