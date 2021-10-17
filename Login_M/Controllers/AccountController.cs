using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_M.Models;
using System.Data.SqlClient; 

namespace Login_M.Controllers
{
   
      
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connetcionString()
        {
            con.ConnectionString = @"Data source=HUZEYFE\SQLEXPRESS; Database=WPF; integrated security = SSPI;";
        }
       
        [HttpPost]

        

        public ActionResult Verify(Account acc)
        {
            connetcionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from tbl_login where usarname ='"+acc.Name+"' and password ='"+acc.Password+"'"  ;
            dr = com.ExecuteReader();
            if (dr.Read()) 
            {
                con.Close();
                return View("../Account/Create");
            }
            else
            {
                con.Close();
                return View("Error");
            }
            
            
            
        }

        [HttpPost]
        public ActionResult Ekleme(AccountInsertModel acc)
        {
            connetcionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO tbl_list(Ad, Soyad, Telefon) VALUES('" + acc.Ad + "', '" + acc.Soyad + "', '" + acc.Telefon + "')";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return View("Error");

            }
            else
            {
                con.Close();
                ListAll();
                return View("../Account/ListAll");


            }



        }
       [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetListById(int id)
        {
            return View();
        }
        [HttpGet]
        [Route("list")]
        public ActionResult ListAll()
        {
            #region data
            List<Personel> p = new List<Personel>();
            connetcionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM tbl_list";
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                if (rdr.HasRows) {

               while (rdr.Read())
                {
                    String ad = rdr.GetString(0);
                    String soyad = rdr.GetString(1);
                    String tel = rdr.GetString(2);
                    p.Add(new Personel
                    {
                        Ad = ad,
                        Soyad = soyad,
                        Telefon = tel
                    });
                    
                }
            }
            }
            con.Close();

            //p.Add(new Personel { Id = 1, Ad = "ahmet", Soyad = "başdan", KimlikNo = 943456489, MedeniDurum = "bekar", Il = "Düzce", Ilce = "Cumayeri", Yas = 21 });
            //p.Add(new Personel { Id = 2, Ad = "mehmet", Soyad = "yılmaz", KimlikNo = 746483459, MedeniDurum = "evli", Il = "İstanbul", Ilce = "Üsküdar", Yas = 54 });
            //p.Add(new Personel { Id = 3, Ad = "ali", Soyad = "bulut", KimlikNo = 985456489, MedeniDurum = "evli", Il = "Düzce", Ilce = "Kaynaşlı", Yas = 32 });
            //p.Add(new Personel { Id = 4, Ad = "haydar", Soyad = "koç", KimlikNo = 489456489, MedeniDurum = "bekar", Il = "Amasya", Ilce = "Merzifon", Yas = 45 });
            //p.Add(new Personel { Id = 5, Ad = "kemal", Soyad = "su", KimlikNo = 562456489, MedeniDurum = "evli", Il = "Düzce", Ilce = "Kaynaşlı", Yas = 32 });
            //p.Add(new Personel { Id = 6, Ad = "cihan", Soyad = "pak", KimlikNo = 456456489, MedeniDurum = "bekar", Il = "Kastamonu", Ilce = "Tosya", Yas = 28 });
            //p.Add(new Personel { Id = 7, Ad = "engin", Soyad = "öztürk", KimlikNo = 489643456, MedeniDurum = "bekar", Il = "Bolu", Ilce = "Gerede", Yas = 19 });
            //p.Add(new Personel { Id = 8, Ad = "sefa", Soyad = "eker", KimlikNo = 453456489, MedeniDurum = "evli", Il = "Bilecik", Ilce = "Merkez", Yas = 49 });
            //p.Add(new Personel { Id = 9, Ad = "ahmet", Soyad = "başdan", KimlikNo = 345956456, MedeniDurum = "bekar", Il = "Ankara", Ilce = "Merkez", Yas = 53 });
            #endregion
            PersonelListViewModel listViewModel = new PersonelListViewModel
            {
                personels = p
            };
            return View(listViewModel);
        }
    }
}