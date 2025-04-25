using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace G11_Final_MedicalApp
{
    public class RappelService : IRappelService
    {
        private readonly List<System.Timers.Timer> Timers = new();

        public event EventHandler<RendezVous>? RappelArrive;
        public event EventHandler<RendezVous> RdvEnApproche;

        public void PlanifierRappel(RendezVous rdv, TimeSpan avant)
        {
            // Calcul du délai avant le rappel
            var rappelAt = rdv.DateDeRdv - avant;
            var delay = rappelAt - DateTime.Now;

            // Si c'est déjà passé, on ne planifie pas
            if (delay.TotalMilliseconds <= 0)
                return;

            // Timer.Interval (en ms) doit être <= Int32.MaxValue
            const double MAX = Int32.MaxValue;
            var ms = Math.Min(delay.TotalMilliseconds, MAX);

            var timer = new System.Timers.Timer(ms)
            {
                AutoReset = false
            };
            timer.Elapsed += (s, e) =>
            {
                RappelArrive?.Invoke(this, rdv);
                timer.Dispose();
            };
            timer.Start();
            Timers.Add(timer);
        }

        public void Dispose()
        {
            foreach (var t in Timers) t.Dispose();
            Timers.Clear();
        }
    }
}
