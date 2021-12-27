using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ARQ
{
    public class TransmissionChannel
    {
        public static List<Frame> FramesRecipient { get; set; }

        public static List<Frame> FramesSender { get; set; }

        public double FrameCount { get; set; }

        public double Corrupted { get; set; }

        public Recipient Recipient { get; set; }

        private Task gMessage;
        private Task info;

        public TransmissionChannel(Recipient recipient)
        {
            FramesRecipient = new List<Frame>();
            FramesSender = new List<Frame>();
            FrameCount = 0;
            Recipient = recipient;
        }

        public void CheckFrame(CancellationToken token)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            gMessage = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (FramesRecipient.Any())
                    {
                        var frames = new List<Frame>(FramesRecipient);
                        foreach (var frame in frames)
                        {
                            if (frame.IsCorrupted)
                            {
                                Corrupted++;
                            }
                            FramesRecipient.Remove(frame);
                        }
                    }
                }
            }, token);
        }

        public async void TranFrame(Frame frame)
        {
            await Task.Delay(TimeSpan.FromSeconds(0));
            FrameCount++;
            if (frame.IsCorrupted)
            {
                Corrupted++;
                Console.WriteLine(@"Corrupted frame {0}", frame.Number);
                return;
            }
            Console.WriteLine(@"Add frame to Recipient {0}", frame.Number);
            if (FramesRecipient.Contains(frame))
            {
                Corrupted++;
                Console.WriteLine("Такой кадр уже есть");
            }
            else
            {
                FramesRecipient.Add(frame);
            }
        }

        public void CheckCorp(CancellationToken token)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            info = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    double asas = FrameCount != 0 && Corrupted != 0 ? Corrupted / FrameCount : 0;
                    Console.WriteLine(@"Satistic {0}", asas);
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }, token);
        }
    }

    public class Frame
    {
        public bool IsCorrupted { get; set; }

        public int Number { get; set; }

        public string Id { get; set; }

        public Frame()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj) => Equals(obj as Frame);

        public bool Equals(Frame frame)
        {
            return Number == frame.Number && IsCorrupted == frame.IsCorrupted && Id==frame.Id;
        }
    }

    public class ASK : Frame
    {

    }
}
