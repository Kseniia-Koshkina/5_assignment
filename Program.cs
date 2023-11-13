namespace _5_assignment
{
    public class Program
    {
        public interface Subject
        {
            void subscribe(Observer observer);
            void remove(Observer observer);
            void notify();
        }
        public class WeatherData : Subject
        {
            private float temperature;
            private float humidity; 
            private float pressure;

            private List<Observer> _observers = new List<Observer>();
            public void subscribe(Observer observer) => _observers.Add(observer);
            public void remove(Observer observer) => _observers.Remove(observer);

            public float getTemperature() => temperature;
            public float getHumidity() => humidity;
            public float getPressure() => pressure;

            public void notify()
            {
                foreach (var item in _observers)
                {
                    item.update(this.temperature,this.humidity,this.pressure);
                }
            }
            void measurementsChanged() => notify();
            public void setMeasurements(float temp, float hum, float press)
            {
                this.temperature = temp;
                this.humidity = hum;
                this.pressure = press;
                measurementsChanged();
            }
        }

        public interface Observer
        {
            public void update(float temp, float hum, float press);
        }

        public interface DisplayElement
        {
            public void display();
        }

        public class CurrentConditionsDisplay : Observer, DisplayElement
        {
            private float temperature;
            private float humidity;
            private float pressure;
            private WeatherData weatherData;

            public CurrentConditionsDisplay(WeatherData weatherData)
            {
                this.weatherData = weatherData;
                weatherData.subscribe(this);
            }
            public CurrentConditionsDisplay() { }

            public void display() => Console.WriteLine("Current conditions: {1} degrees and {0}" +
                     " % himodity\nAvg/Max/Min temperature {1}/{1}/{1}", this.humidity, this.temperature);
        
            public void update(float temp, float hum, float press)
            {
                this.temperature = temp;
                this.humidity = hum;
                this.pressure = press;
                display();
            }
        }




        static void Main(string[] args)
        {
            WeatherData weatherData = new WeatherData();
            CurrentConditionsDisplay currentConditionsDisplay = new CurrentConditionsDisplay();
            weatherData.subscribe(currentConditionsDisplay);
            weatherData.setMeasurements(80, 65, 30.4f);

        }
    }
}