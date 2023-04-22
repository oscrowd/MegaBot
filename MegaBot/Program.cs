using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBot;
using Telegram.Bot;

namespace VoiceTexterBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // ������, ���������� �� ���������� ��������� ���� ����������
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // ������ ������������
                .UseConsoleLifetime() // ��������� ������������ ���������� �������� � �������
                .Build(); // ��������

            Console.WriteLine("������ �������");
            // ��������� ������
            await host.RunAsync();
            Console.WriteLine("������ ����������");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // ������������ ������ TelegramBotClient c ������� �����������
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6101708551:AAFCaBom8KE-zcK4NCOLV-0JpatTUXwipbU"));
            // ������������ ��������� �������� ������ ����
            services.AddHostedService<Bot>();
        }
    }
}