
using CustomerManagementSystem.Business;
using CustomerManagementSystem.Business.Validations;
using CustomerManagementSystem.DataAccess;
using CustomerManagementSystem.Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace  CustomerManagementSystem.UserInterface
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // DbContext
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer("Server=ORAY\\SQLEXPRESS;Database=OrayDb;Trusted_Connection=True;TrustServerCertificate=True;"));

                    // AutoMapper
                    services.AddAutoMapper(typeof(MappingProfile));
                    services.AddLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddDebug();
                    });

                    // Business katmanı servisleri
                    services.AddScoped<ICustomerRepository<Customer>, CustomerRepository>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<ICustomerService, CustomerService>();
                    services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();
                   
                   
                });

            var host = builder.Build();


            using (var scope = host.Services.CreateScope())
            {

                var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                var validator = scope.ServiceProvider.GetRequiredService<IValidator<CustomerDto>>();


                // Basit Konsol Arayüzü - Döngü ile Menü
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

                    // Müşteri Ekleme
                    if (secim == "1")
                    {
                        Console.Write("Ad: ");
                        var name = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Email: ");
                        var email = Console.ReadLine();
                        Console.WriteLine();

                        await service.AddCustomerAsync(new CustomerDto { Name = name, Email = email });
                        

                    }

                    // Tüm Müşteri İsimlerini Listeleme
                    else if (secim == "2")
                    {
                        var customers = await service.GetAllCustomersAsync();
                        Console.WriteLine("ID - Name - Email\n");
                        foreach (var customer in customers)
                        {
                            Console.WriteLine($"{customer.Id} - {customer.Name} - {customer.Email}");
                        }
                        Console.WriteLine("\n");
                    }


                    // Müşteri Güncelleme
                    else if (secim == "3")
                    {
                        Console.Write("Güncellenecek müşteri ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var existing = await service.GetCustomerByIdAsync(id);
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

                            ValidationResult result = await validator.ValidateAsync(existing);
                            if (!result.IsValid)
                            {
                                foreach (var error in result.Errors)
                                    Console.WriteLine($"Hata: {error.ErrorMessage}");
                                continue;
                            }


                            await service.UpdateCustomerAsync(existing);
                            
                        }
                    }

                    // Müşteri Silme
                    else if (secim == "4")
                    {
                        Console.Write("Silinecek müşteri ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var dto = new CustomerDto { Id = id };
                            ValidationResult result = await validator.ValidateAsync(dto);
                            if (!result.IsValid)
                            {
                                foreach (var error in result.Errors)
                                    Console.WriteLine($"Hata: {error.ErrorMessage}");
                                continue;
                            }

                            await service.DeleteCustomerAsync(id);

                        }
                    }

                    // Çıkış
                    else if (secim == "5")
                    {
                        break;
                    }

                    // Hatalı Seçim
                    else
                    {
                        Console.WriteLine("Geçersiz seçim\n");

                    }
                }
            }
        }
    }
}
