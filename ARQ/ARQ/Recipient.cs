using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ARQ
{
    public class Recipient
    {
        public List<Frame> Frames;
        private int Intensity { get; set; }

        private Task gMessage;

        public Recipient(int intensity)
        {
            Frames = new List<Frame>();
            Intensity = intensity;
        }

        public void GetFrame22(CancellationToken token)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            gMessage = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    var frame = Frames.FirstOrDefault();
                    if (frame != default)
                    {

                    }

                    await Task.Delay(TimeSpan.FromSeconds(Intensity));
                }
            });
        }

        public void AddFrame(Frame frame)
        {
            Frames.Add(frame);
        }

        public void GetFrame(CancellationToken token)
        {
            gMessage = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

                    var frame = TransmissionChannel.FramesRecipient.FirstOrDefault();
                    if (frame != default)
                    {
                        if (frame.Number == 0 && Frames.Count != 1)
                        {
                            Frames = new();
                        }
                        await Task.Delay(TimeSpan.FromSeconds(Intensity));
                        TransmissionChannel.FramesRecipient.Remove(frame);
                        if (!Frames.Contains(frame))
                        {
                            Console.WriteLine("Recipient " + frame.Number + " complete");
                            Frames.Add(frame);
                            var ask = new ASK
                            {
                                IsCorrupted = Convert.ToBoolean(rnd.Next(0, 1)),
                                Number = frame.Number,
                                Id = frame.Id,
                            };
                            Console.WriteLine(@"Send ask {0}", ++ask.Number);
                            TransmissionChannel.FramesSender.Add(ask);
                            if (ask?.IsCorrupted ?? false)
                            {
                                TransmissionChannel.FramesSender.Remove(ask);
                                Console.WriteLine("Corrupted ask {0}", ask.Number);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Уже есть такой кадр, ask отправка");
                            var ask = new ASK
                            {
                                IsCorrupted = Convert.ToBoolean(rnd.Next(0, 1)),
                                Number = frame.Number,
                                Id = frame.Id,
                            };
                            Console.WriteLine(@"Send ask {0}", ++ask.Number);
                            TransmissionChannel.FramesSender.Add(ask);
                            if (ask?.IsCorrupted ?? false)
                            {
                                TransmissionChannel.FramesSender.Remove(ask);
                                Console.WriteLine("Corrupted ask {0}", ask.Number);
                            }
                        }

                    }
                }

            });
        }
    }
}
