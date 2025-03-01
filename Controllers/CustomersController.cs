using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVVMExample.DataAccess;
using MVVMExample.Models.ViewModels;

namespace MVVMExample.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(SearchViewModel nameSearchViewModel)
        {
            if (ModelState.IsValid)
            {
                string searchString = nameSearchViewModel.NameSearchString;
                List<Customer> searchResults = _context.Customers.Where(s => 
                s.FirstName.Contains(searchString) || s.LastName.Contains(searchString)).ToList();

                if (searchResults.Count == 0)
                {
                    ModelState.AddModelError("NameSearchString", "No customer name contains the specified characters");
                }
                else
                {
                    ViewData["searchString"] = searchString;
                    return View("Index", searchResults);
                }
            }
            return View(nameSearchViewModel);
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Customers.ToListAsync());
        }

        

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customerVM)
        {
            if (_context.Customers.Any(c => c.Id == customerVM.Customer.Id))
            {
                ModelState.AddModelError("Customer.Id", "A customer with this ID already exists in the system");
            }
            if (ModelState.IsValid)
            {
                Customer customer = customerVM.Customer;
                if (customerVM.BillingAddress.Address1 != null && customerVM.BillingAddress.City != null && customerVM.BillingAddress.ProvinceCode != null)
                {
                    customerVM.BillingAddress.AddressType = "Billing";
                    customer.Addresses.Add(customerVM.BillingAddress);
                }

                if (customerVM.ShippingAddress.Address1 != null && customerVM.ShippingAddress.City != null && customerVM.ShippingAddress.ProvinceCode != null)
                {
                    customerVM.ShippingAddress.AddressType = "Shipping";
                    customer.Addresses.Add(customerVM.ShippingAddress);
                }
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerVM);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(c=>c.Addresses).SingleOrDefaultAsync(c=>c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            CustomerViewModel customerVM = new CustomerViewModel(customer);
            return View(customerVM);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(/*int id,*/ CustomerViewModel customerVM)
        {
            //if (id != customerVM.Customer.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
               
                Customer customer = _context.Customers.Include(c=>c.Addresses).SingleOrDefault(c => c.Id == customerVM.Customer.Id);
                if (customer != null)
                {
                    customer.FirstName = customerVM.Customer.FirstName;
                    customer.LastName = customerVM.Customer.LastName;
                    customer.Addresses.Clear();

                    if (customerVM.BillingAddress.Address1 != null && customerVM.BillingAddress.City != null && customerVM.BillingAddress.ProvinceCode != null)
                    {
                        customerVM.BillingAddress.AddressType = "Billing";
                        customer.Addresses.Add(customerVM.BillingAddress);
                    }

                    if (customerVM.ShippingAddress.Address1 != null && customerVM.ShippingAddress.City != null && customerVM.ShippingAddress.ProvinceCode != null)
                    {
                        customerVM.ShippingAddress.AddressType = "Shipping";
                        customer.Addresses.Add(customerVM.ShippingAddress);
                    }

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
             
                return RedirectToAction(nameof(Index));
            }
            return View(customerVM);
        }

        private bool CustomerExists(int id)
        {
          return _context.Customers.Any(e => e.Id == id);
        }
    }
}
