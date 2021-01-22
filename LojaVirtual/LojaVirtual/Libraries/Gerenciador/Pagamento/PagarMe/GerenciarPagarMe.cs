﻿using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using PagarMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Gerenciador.Pagamento.PagarMe
{
    public class GerenciarPagarMe
    {
        private IConfiguration _configuration;
        private LoginCliente _loginCliente;
        public GerenciarPagarMe(IConfiguration configuration, LoginCliente loginCliente)
        {
            _configuration = configuration;
            _loginCliente = loginCliente;
        }
        public object GerarBoleto(decimal valor)
        {
            try
            {
                Cliente cliente = _loginCliente.GetCliente();
                PagarMeService.DefaultApiKey = _configuration.GetValue<String>("Pagamento:PagarMe:ApiKey:");
                PagarMeService.DefaultEncryptionKey = _configuration.GetValue<String>("Pagamento:PagarMe:EncryptionKey:");

                Transaction transaction = new Transaction();

                //TODO - Calcular o valor total
                transaction.Amount = Convert.ToInt32(valor);
                transaction.PaymentMethod = PaymentMethod.Boleto;

                transaction.Customer = new Customer
                {
                    ExternalId = cliente.Id.ToString(),
                    Name = cliente.Nome,
                    Type = CustomerType.Individual,
                    Country = "br",
                    Email = cliente.Email,
                    Documents = new[]
                        {
                    new Document{
                      Type = DocumentType.Cpf,
                      //TODO - Remover Mascara no CPF
                      Number = cliente.CPF
                    }
                    },
                    PhoneNumbers = new string[]
                    {
                        //Remover Mascara no Telefone
                      "+55" + cliente.Telefone
                    },
                    Birthday = cliente.Nascimento.ToString("yyyy-MM-dd")
                };

                transaction.Save();

                return new { BoletoURL = transaction.BoletoUrl, Barcode = transaction.BoletoBarcode, Expiracao = transaction.BoletoExpirationDate };
            }
            catch(Exception e)
            {
                return new { Erro = e.Message };
            }
            //transaction.Billing = new Billing
            //{
            //    Name = "Morty",
            //    Address = new Address()
            //    {
            //        Country = "br",
            //        State = "sp",
            //        City = "Cotia",
            //        Neighborhood = "Rio Cotia",
            //        Street = "Rua Matrix",
            //        StreetNumber = "213",
            //        Zipcode = "04250000"
            //    }
            //};

            //var Today = DateTime.Now;

            //transaction.Shipping = new Shipping
            //{
            //    Name = "Rick",
            //    Fee = 100,
            //    DeliveryDate = Today.AddDays(4).ToString("yyyy-MM-dd"),
            //    Expedited = false,
            //    Address = new Address()
            //    {
            //        Country = "br",
            //        State = "sp",
            //        City = "Cotia",
            //        Neighborhood = "Rio Cotia",
            //        Street = "Rua Matrix",
            //        StreetNumber = "213",
            //        Zipcode = "04250000"
            //    }
            //};

            //transaction.Item = new[]
            //{
            //  new Item()
            //  {
            //    Id = "1",
            //    Title = "Little Car",
            //    Quantity = 1,
            //    Tangible = true,
            //    UnitPrice = 1000
            //  },
            //  new Item()
            //  {
            //    Id = "2",
            //    Title = "Baby Crib",
            //    Quantity = 1,
            //    Tangible = true,
            //    UnitPrice = 1000
            //  }
            //};

        }
    }
}
