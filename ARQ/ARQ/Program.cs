using System;
using System.Threading;

namespace ARQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Recipient recipient = new Recipient(2);
            TransmissionChannel transmission = new TransmissionChannel(recipient);

            Sender sender = new Sender(2, 3, transmission);
            sender.GenerateFrame(cancellationTokenSource.Token);
            transmission.CheckCorp(cancellationTokenSource.Token);
            recipient.GetFrame(cancellationTokenSource.Token);

            Console.ReadKey();
        }
    }
}
