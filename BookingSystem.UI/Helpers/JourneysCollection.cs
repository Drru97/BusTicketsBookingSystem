using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.UI.Helpers
{
    public class JourneysCollection
    {
        private IEnumerable<Journey> _journeys;
        private int _price;

        public IEnumerable<Journey> Journeys
        {
            get { return _journeys; }
            set { _journeys = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public JourneysCollection(IEnumerable<Journey> journeys, int _price)
        {
            Journeys = journeys;
            Price = _price;
        }
    }
}
