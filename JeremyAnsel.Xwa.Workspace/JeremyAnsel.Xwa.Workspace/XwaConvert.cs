namespace JeremyAnsel.Xwa.Workspace
{
    public static class XwaConvert
    {
        public static float ToMeters(float raw)
        {
            return raw / 40.96f;
        }

        public static float FromMeters(float value)
        {
            return value * 40.96f;
        }

        public static int ToMglt(int raw)
        {
            return (int)(raw / 2.25f + 0.5f);
        }

        public static int FromMglt(int value)
        {
            return (int)(value * 2.25f + 0.5f);
        }

        public static int ToDpf(int roll, int pitch)
        {
            return (int)((roll + pitch) * 0.0052315756f + 0.5f);
        }

        public static int ToSbd(XwaShipCategory shipCategory, int raw)
        {
            int value = raw / 50;

            switch (shipCategory)
            {
                case XwaShipCategory.Starship:
                case XwaShipCategory.Platform:
                    value *= 16;
                    break;

                case XwaShipCategory.Freighter:
                case XwaShipCategory.Container:
                    value *= 4;
                    break;
            }

            return value;
        }

        public static int FromSbd(XwaShipCategory shipCategory, int value)
        {
            int raw = value * 50;

            switch (shipCategory)
            {
                case XwaShipCategory.Starship:
                case XwaShipCategory.Platform:
                    raw /= 16;
                    break;

                case XwaShipCategory.Freighter:
                case XwaShipCategory.Container:
                    raw /= 4;
                    break;
            }

            return raw;
        }

        public static int ToRu(XwaShipCategory shipCategory, int raw)
        {
            int value = raw / 105;

            switch (shipCategory)
            {
                case XwaShipCategory.Starship:
                case XwaShipCategory.Platform:
                    value *= 16;
                    break;

                case XwaShipCategory.Freighter:
                case XwaShipCategory.Container:
                    value *= 4;
                    break;
            }

            return value;
        }

        public static int FromRu(XwaShipCategory shipCategory, int value)
        {
            int raw = value * 105;

            switch (shipCategory)
            {
                case XwaShipCategory.Starship:
                case XwaShipCategory.Platform:
                    raw /= 16;
                    break;

                case XwaShipCategory.Freighter:
                case XwaShipCategory.Container:
                    raw /= 4;
                    break;
            }

            return raw;
        }
    }
}
