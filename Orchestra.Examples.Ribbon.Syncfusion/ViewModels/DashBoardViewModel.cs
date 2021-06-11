using Catel.MVVM;
using System;
using System.Windows.Threading;

namespace Orchestra.Examples.Ribbon.ViewModels
{
    class DashBoardViewModel : ViewModelBase
    {
        #region Fields & Properties

        readonly Random _randomTorque = new Random();
        private bool _reverse = false;
        private double _speedTemp = 0;
        private double _gear = 1;
        private double _Rpm = 0;

        public DispatcherTimer Timer { get; set; }

        public double Speed { get; set; }

        public double RPM { get; set; }

        public double Temperature { get; set; }
        void OnTemperatureChanged()
        {
            TemperatureText = Math.Round(Temperature).ToString();
        }

        public string TemperatureText { get; set; }

        public string FuelGaugeText { get; set; }

        public double Fuel { get; set; }

        void OnFuelChanged()
        {
            FuelGaugeText = Math.Round(Fuel).ToString();
        }

        public double Torque { get; set; }

        #endregion

        #region Constructor

        public DashBoardViewModel()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            Timer.Tick += timer_Tick;

            Fuel = 100;
            Speed = 0;
            RPM = 0;
            Temperature = 0;
            Torque = 0.3;
            Timer.Start();
        }

        #endregion

        #region Methods

        void ReverseTimer_Tick()
        {
            _speedTemp -= .25 * _gear * 2;
            Speed = _speedTemp;
            if (Speed <= 80)
            {
                _reverse = false;
                _gear = 2;
            }
        }

        void timer_Tick(object sender, object e)
        {
            if (Fuel > 0 && Temperature < 80)
            {
                if (Temperature > 35)
                {
                    Temperature += .05;
                }
                else
                {
                    Temperature += 1;
                }

                if (RPM >= 6.2)
                {
                    if (_gear < 5)
                    {
                        _gear += 1;
                        _Rpm -= 3;
                        RPM = _Rpm;
                    }
                    else if (RPM > 6.5)
                    {
                        _Rpm = 6.2;
                        RPM = _Rpm;
                    }
                    else
                    {
                        _Rpm += 0.1;
                        RPM = _Rpm;
                    }
                }
                else
                {
                    _Rpm += 0.1;
                    RPM = _Rpm;
                }


                if (Speed >= 80)
                {

                    if (Speed >= 140 || _reverse)
                    {
                        _reverse = true;
                        ReverseTimer_Tick();

                    }
                    else
                    {
                        _speedTemp += .25 * _gear;
                        Speed = _speedTemp;
                    }
                }
                else if (!_reverse)
                {
                    _speedTemp += .25 * _gear;
                    Speed = _speedTemp;
                }
            }
            else
            {
                _speedTemp = 0;
                Speed = 0;
                _Rpm = 0;
                RPM = 0;
                _gear = 1;
                Temperature = 0;
                Torque = 0;
                _reverse = false;
                if (Fuel == 0 || Temperature == 85)
                {
                    Fuel = 100;
                    Temperature = 0;
                }
            }
            Fuel -= .25;
            int TorqueMedium = _randomTorque.Next(40, 50);
            Torque = Speed / (2 * TorqueMedium);
            Torque = Math.Round(Torque, 1, MidpointRounding.AwayFromZero);
        }

        #endregion
    }
}
