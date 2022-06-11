using System;
using System.Diagnostics;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        //Usando o driver do edge e previnir de apresentar logs
        EdgeDriverService service = EdgeDriverService.CreateDefaultService();
        service.EnableVerboseLogging = false;
        service.SuppressInitialDiagnosticInformation = true;
        service.HideCommandPromptWindow = true;

        var options = new EdgeOptions();
        options.PageLoadStrategy = PageLoadStrategy.Normal;
        options.AddArguments("headless");
        options.AddArgument("--disable-logging");
        options.AddArgument("--log-level=3");
        options.AddArgument("--blink-settings=imagesEnabled=false");
        var driver = new EdgeDriver(service, options);
        
        //Usuario digita o estado
        Console.WriteLine("Digite o nome do seu Estado/Cidade: ");
        string estado = Console.ReadLine();
        
        try
        {
            //Busca pelo estado e navega até ele
            driver.Navigate().GoToUrl("https://www.google.com/search?q=clima+agora+" + estado
                .ToLower()   
                .Replace(" ","+"));
            Thread.Sleep(500);

            //Pegar as informações do tempo
            var cidadenome = driver.FindElement(By.XPath("//*[@id='wob_loc']")).Text;
            var tempmin = driver.FindElement(By.XPath("//*[@id='wob_dp']/div[1]/div[3]/div[2]/span[1]")).Text;
            var tempmax = driver.FindElement(By.XPath("//*[@id='wob_dp']/div[1]/div[3]/div[1]/span[1]")).Text;
            var chuva = driver.FindElement(By.XPath("//*[@id='wob_pp']")).Text;
            var vento = driver.FindElement(By.XPath("//*[@id='wob_ws']")).Text;
            var umidade = driver.FindElement(By.XPath("//*[@id='wob_hm']")).Text;
            var temperatura = driver.FindElement(By.XPath("//*[@id='wob_tm']")).Text;
            var infohoje = driver.FindElement(By.XPath("//*[@id='wob_dc']")).Text;
            var tempo = driver.FindElement(By.XPath("//*[@id='wob_dts']")).Text;

            Console.Clear();
            
            //Imprimir as informações
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Temperatura: {temperatura}°C   🏙 Cidade: {cidadenome}");
            Console.WriteLine($"\u2B06 Minima: {tempmin} \u2B07 Maxima: {tempmax}");
            Console.WriteLine("\u2614 Chuva: " + chuva);
            Console.WriteLine("🍃 Vento: " + vento);
            Console.WriteLine("Umidade: " + umidade);
            Console.WriteLine("Situação: " + infohoje);
            Console.WriteLine(tempo);
            Console.ReadKey();
        }
        //Caso não encontre a cidade ou estado
        catch (Exception e)
        {
            Console.WriteLine("Cidade/Estado não encontrado, tente novamente");
            Console.ReadKey();
            Console.Clear();
            Main(null);
        }
        //Fechar o driver
        driver.Close();
    }
}