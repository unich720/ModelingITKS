using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ARQ
{
    public class Sender
    {
        private int Intensity { get; set; }
        private int NumberOfFrames { get; set; }
        private TransmissionChannel Transmission { get; set; }

        private Task gMessage;

        public Sender(int intensity, int numberOfFrames, TransmissionChannel transmission)
        {
            Intensity = intensity;
            NumberOfFrames = numberOfFrames;
            Transmission = transmission;
        }

        public void GenerateFrame(CancellationToken token)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            gMessage = Task.Run(async () =>
            {
                double count = 0;
                double sum = 0;
                while (!token.IsCancellationRequested)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    count++;
                    Console.WriteLine("Count " + count);
                    TransmissionChannel.FramesSender = new();
                    for (int i = 0; i < NumberOfFrames; i++)
                    {
                        var frame = new Frame();
                        while (true)
                        {
                            frame.IsCorrupted = Convert.ToBoolean(rnd.Next(0, 1));
                            frame.Number = i;

                            Console.WriteLine("Frame " + i + " max " + NumberOfFrames);
                            Transmission.TranFrame(frame);
                            await Task.Delay(TimeSpan.FromSeconds(Intensity));

                            var ask = TransmissionChannel.FramesSender.FirstOrDefault(x => x.Number == (i + 1));
                            if (ask != default && ask.Number == i + 1)
                            {
                                Console.WriteLine("ask {0} принят", ask.Number);
                                TransmissionChannel.FramesSender.Remove(ask);
                                break;
                            }
                        }

                        //bool tt = false;
                        //while (!tt)
                        //{
                        //    Console.WriteLine("Frame " + i + " max " + NumberOfFrames);
                        //    tt = await Transmission.TranFrame(
                        //    new Frame
                        //    {
                        //        IsCorrupted = Convert.ToBoolean(rnd.Next(0, 1)),
                        //        Number = i,
                        //    });
                        //    if (!tt)
                        //    {
                        //        Console.WriteLine("frame или ask потерян ");
                        //    }
                        //}

                    }
                    stopwatch.Stop();
                    sum += stopwatch.Elapsed.Seconds;
                    Console.WriteLine(@"Stat {0}, count {1}", sum / count, count);
                    //await Task.Delay(TimeSpan.FromSeconds(Intensity));
                }
            });
        }
    }
}
