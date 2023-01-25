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
        private List<int> subcategories;
        [OneToMany]
        public List<int> Subcategories { get { return subcategories; } set { subcategories = value; OnPropertyChanged(); } }
        private Category catFather;
        private int catFatherId;
        [ForeignKey(typeof(int))]
        public int CatFatherId { get { return catFatherId; } set { catFatherId = value; OnPropertyChanged(); } }
        [ManyToOne]
        public Category CatFather { get { return catFather; } set { catFather = value; OnPropertyChanged(); } }

        private List<int> products;
        [ManyToMany(typeof(CategoryProduct))]
        public List<int> Products { get { return products; } set { products = value; OnPropertyChanged(); } }
        public Category()
        {
            Subcategories = new List<int>();
        }
    }
}
