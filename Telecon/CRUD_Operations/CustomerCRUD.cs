using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telecon.Models;
using Telecon.Data_Formatting;

namespace Telecon.CRUD_Operations
{
    public class CustomerCRUD
    {
        DataFormats df = new DataFormats();

        public bool GetCustomer(string customer, string employee)
        {
            bool found = false;
            string customerName = df.AddressCorrector(customer);
            string employeeName = df.FirstLetterToUpper(employee);
            using(var context = new DataContext())
            {
                var employeeID = (from s in context.Usuarios where s.username == employeeName select s.ID).SingleOrDefault();
                var search = (from s in context.Clientes where s.razonsocial == customerName && s.employeeID == employeeID
                              select s.razonsocial).SingleOrDefault();

                if(search != null)
                {
                    found = true;
                }
                else
                {
                    found = false;
                }
            }
            return found;
        }


        public void AddCustomer(Customer model, string employee)
        {
            string employeeName = df.FirstLetterToUpper(employee);
            string nombre = null, apellido = null, comentario = null;
            nombre = model.nombres;
            apellido = model.apellidos;
            comentario = model.comentarios;
            using(var context = new DataContext())
            {
                int employeeFK = (from s in context.Usuarios where s.username == employeeName select s.ID).SingleOrDefault();
                var customer = new Customer
                {
                    employeeID = employeeFK,
                    razonsocial = df.AddressCorrector(model.razonsocial),
                    nombres = df.FirstLetterToUpper(nombre),
                    apellidos = df.FirstLetterToUpper(apellido),
                    direccion = df.AddressCorrector(model.direccion),
                    telefono = model.telefono,
                    comentarios = df.FirstLetterToUpper(comentario)
                };
                context.Clientes.Add(customer);
                context.SaveChanges();
            }
        }

        public void EditCustomer(int customerID, Customer model, string employee)
        {
            string employeeName = df.FirstLetterToUpper(employee);
            string nombre = null, apellido = null, comentario = null;
            nombre = model.nombres;
            apellido = model.apellidos;
            comentario = model.comentarios;

            using (var context = new DataContext())
            {
                int employeeFK = (from s in context.Usuarios where s.username == employeeName select s.ID).SingleOrDefault();
                var customer = (from s in context.Clientes where s.employeeID == employeeFK && s.ID == customerID select s).FirstOrDefault();
                customer.razonsocial = df.AddressCorrector(model.razonsocial);
                customer.nombres = df.FirstLetterToUpper(nombre);
                customer.apellidos = df.FirstLetterToUpper(apellido);
                customer.direccion = df.AddressCorrector(model.direccion);
                customer.telefono = model.telefono;
                customer.comentarios = df.FirstLetterToUpper(comentario);

                context.SaveChanges();
            }
        }

        public void DeleteCustomers(string[] customersList, string employee)
        {
            string employeeName = df.FirstLetterToUpper(employee);
            using(var context = new DataContext())
            {
                int employeeID = (from s in context.Usuarios where s.username == employeeName select s.ID).SingleOrDefault();
                for(int i = 0; i < customersList.Length; i++)
                {
                    string customerName = df.FirstLetterToUpper(customersList[i]);
                    var customer = (from s in context.Clientes
                                    where s.razonsocial == customerName && s.employeeID == employeeID
                                    select s).FirstOrDefault();
                    context.Clientes.Remove(customer);
                }
                context.SaveChanges();
            }
        }
    }
}