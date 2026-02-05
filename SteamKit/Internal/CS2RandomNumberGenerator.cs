
namespace SteamKit.Internal
{
    internal class CS2RandomNumberGenerator
    {
        public readonly int NTAB;
        public readonly double IA;
        public readonly double IM;
        public readonly double IQ;
        public readonly double IR;
        public readonly double NDIV;
        public readonly double AM;
        public readonly double RNMX;

        public double mIdum;
        public double mIy;
        public double[] mIv;

        public CS2RandomNumberGenerator()
        {
            this.NTAB = 32;
            this.IA = 16807;
            this.IM = 2147483647;
            this.IQ = 127773;
            this.IR = 2836;
            this.NDIV = 1 + (this.IM - 1) / this.NTAB;
            this.AM = 1.0f / this.IM;
            this.RNMX = 1.0f - 1.2e-7f;

            this.mIdum = 0;
            this.mIy = 0;
            this.mIv = new double[NTAB];
        }

        public void SetSeed(double seed)
        {
            this.mIdum = seed;
            if (seed >= 0)
            {
                this.mIdum = -seed;
            }

            this.mIy = 0;
        }

        public double GenerateRandomNumber()
        {
            double k;
            int j;

            if (this.mIdum <= 0 || this.mIy == 0)
            {
                if (-this.mIdum < 1)
                {
                    this.mIdum = 1;
                }
                else
                {
                    this.mIdum = -this.mIdum;
                }

                for (j = this.NTAB + 7; j >= 0; j -= 1)
                {
                    k = Math.Floor(this.mIdum / this.IQ);
                    this.mIdum = Math.Floor(this.IA * (this.mIdum - k * this.IQ) - this.IR * k);

                    if (this.mIdum < 0)
                    {
                        this.mIdum += this.IM;
                    }

                    if (j < this.NTAB)
                    {
                        this.mIv[j] = this.mIdum;
                    }
                }

                this.mIy = this.mIv[0];
            }

            k = Math.Floor(this.mIdum / this.IQ);
            this.mIdum = Math.Floor(this.IA * (this.mIdum - k * this.IQ) - this.IR * k);

            if (this.mIdum < 0)
            {
                this.mIdum += this.IM;
            }

            j = (int)Math.Floor(this.mIy / this.NDIV);

            this.mIy = Math.Floor(this.mIv[j]);
            this.mIv[j] = this.mIdum;

            return this.mIy;
        }

        public double Random(double low, double high)
        {
            double value = this.AM * GenerateRandomNumber();

            if (value > this.RNMX)
            {
                value = this.RNMX;
            }

            var result = (value * (high - low)) + low;
            return result;
        }
    }
}
