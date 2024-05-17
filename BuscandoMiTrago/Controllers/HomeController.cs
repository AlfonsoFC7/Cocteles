using BuscandoMiTrago.DtaCtx;
using BuscandoMiTrago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace BuscandoMiTrago.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContextt _db;

        public HomeController(ILogger<HomeController> logger, DbContextt db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index(drinks txtSearch)       
        {
            try
            {
                if(txtSearch.strDrink != null)
                {
                    List<drinks> drinks = new List<drinks>();
                    var client = new HttpClient();
                    var datos = await client.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/search.php?s="+ txtSearch.strDrink);
                    var strData = await datos.Content.ReadAsStringAsync();
                    JObject objeto = JObject.Parse(strData);
                    var drinkss = objeto["drinks"].Children().ToList();
                    ViewBag.drinks = drinkss;
                    foreach (var dri in drinkss)
                    {
                        drinks drii = new drinks();
                        Console.WriteLine(dri);
                    }
                }
                else
                {
                    drinks drii = new drinks();
                    drii.strDrink = "null";
                    List<drinks> drinks = new List<drinks>();
                    var client = new HttpClient();
                    var datos = await client.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/search.php?s=Margarita");
                    var strData = await datos.Content.ReadAsStringAsync();
                    JObject objeto = JObject.Parse(strData);
                    var drinkss = objeto["drinks"].Children().ToList();
                    ViewBag.drinks = drinkss;


                }
                GetTablaDrink();
                //drinks = JsonConvert.DeserializeObject<List<drinks>>(drinkss.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View();
        }

        public async void GetTablaDrink()
        {
            try
            {
                List<drinks> beb = await _db.Drinks.FromSqlRaw($"EXEC SP_TRAGOS @OPC=50").ToListAsync();
                ViewBag.Cocteles = beb;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task<IActionResult> DeleteDrink(drinks dryk)
        {
            try
            {
                await _db.Drinks.FromSqlInterpolated($"EXEC SP_TRAGOS @OPC=3,@idDrink={dryk.idDrink}").ToListAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PostDta(drinks dryk)
        {
            try
            {
             await _db.Drinks.FromSqlInterpolated($"EXEC SP_TRAGOS @OPC=1,@idDrink={dryk.idDrink},  @strDrinkThumb={dryk.strDrinkThumb}, @strDrink={dryk.strDrink}").ToListAsync();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
