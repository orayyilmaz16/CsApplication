using CsApplication.Business;
using CsApplication.Business.Validations;
using CsApplication.DataAccess;
using CsApplication.Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CsApplication.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // DbContext
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer("Server=ORAY\\SQLEXPRESS;Database=OrayDb;Trusted_Connection=True;TrustServerCertificate=True;"));

                    // AutoMapper
                    services.AddAutoMapper(typeof(MappingProfile));

                    // Business katmanı servisleri
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<ICustomerService, CustomerService>();
                    services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();
                });

            var host = builder.Build();


            using (var scope = host.Services.CreateScope())
            {

                var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                while (true)
                {
                    Console.WriteLine("Müşteri Yönetim Sistemine Hoş geldiniz...\n\n");
                    Console.WriteLine("1 - Müşteri Ekle");
                    Console.WriteLine("2 - Tüm Müşterileri Listele");
                    Console.WriteLine("3 - Müşteri Güncelle");
                    Console.WriteLine("4 - Müşteri Sil");
                    Console.WriteLine("5 - Çıkış");


                    Console.WriteLine("\n");
                    Console.Write("Seçim: ");
                    var secim = Console.ReadLine();
                    Console.WriteLine("");

                    if (secim == "1")
                    {
                        Console.Write("Ad: ");
                        var name = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Email: ");
                        var email = Console.ReadLine();
                        Console.WriteLine();

                        service.AddCustomer(new CustomerDto { Name = name, Email = email });
                        Console.WriteLine("Müşteri eklendi\n");

                    }
                    else if (secim == "2")
                    {
                        var customers = service.GetAllCustomers();
                        Console.WriteLine("ID - Name - Email\n");
                        foreach (var customer in customers)
                        {
                            Console.WriteLine($"{customer.Id} - {customer.Name} - {customer.Email}");
                        }
                        Console.WriteLine("\n");
                    }

                    else if (secim == "3")
                    {
                        Console.Write("Güncellenecek müşteri ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var existing = service.GetCustomerById(id);
                            if (existing == null)
                            {
                                Console.WriteLine("Müşteri bulunamadı.");
                                continue;
                            }

                            Console.Write($"Yeni Ad ({existing.Name}): ");
                            var name = Console.ReadLine();
                            Console.Write($"Yeni Email ({existing.Email}): ");
                            var email = Console.ReadLine();

                            existing.Name = string.IsNullOrWhiteSpace(name) ? existing.Name : name;
                            existing.Email = string.IsNullOrWhiteSpace(email) ? existing.Email : email;

                            service.UpdateCustomer(existing);
                            Console.WriteLine("Müşteri güncellendi.");
                        }
                    }
                    else if (secim == "4")
                    {
                        Console.Write("Silinecek müşteri ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            service.DeleteCustomer(id);
                            Console.WriteLine("Müşteri silindi.");
                        }
                    }


                    else if (secim == "5")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçim\n");

                    }
                }
            }
        }
    }
}
