using System;

namespace Util.Tweening
{
    //generated with chat-gpt
    public static class EasingFunctions
    {
        public static float Linear(float t) => t;

        public static float EaseInQuad(float t) => t * t;

        public static float EaseOutQuad(float t) => t * (2 - t);

        public static float EaseInOutQuad(float t) =>
            t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

        public static float EaseInCubic(float t) => t * t * t;

        public static float EaseOutCubic(float t) =>
            (--t) * t * t + 1;

        public static float EaseInOutCubic(float t) =>
            t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;

        public static float EaseInQuart(float t) => t * t * t * t;

        public static float EaseOutQuart(float t) =>
            1 - (--t) * t * t * t;

        public static float EaseInOutQuart(float t) =>
            t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;

        public static float EaseInQuint(float t) => t * t * t * t * t;

        public static float EaseOutQuint(float t) =>
            1 + (--t) * t * t * t * t;

        public static float EaseInOutQuint(float t) =>
            t < 0.5f ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;

        public static float EaseInSine(float t) =>
            (float)(-Math.Cos(t * Math.PI / 2) + 1);

        public static float EaseOutSine(float t) =>
            (float)Math.Sin(t * Math.PI / 2);

        public static float EaseInOutSine(float t) =>
            (float)(-0.5f * (Math.Cos(Math.PI * t) - 1));

        public static float EaseInExpo(float t) =>
            (float)(Math.Pow(2, 10 * (t - 1)));

        public static float EaseOutExpo(float t) =>
            (float)(-Math.Pow(2, -10 * t) + 1);

        public static float EaseInOutExpo(float t) =>
            t < 0.5f ? (float)(Math.Pow(2, 10 * (2 * t - 1)) / 2) : (float)((-Math.Pow(2, -10 * (2 * t - 1)) + 2) / 2);

        public static float EaseInCirc(float t) =>
            (float)(1 - Math.Sqrt(1 - t * t));

        public static float EaseOutCirc(float t) =>
            (float)(Math.Sqrt(1 - (--t) * t));

        public static float EaseInOutCirc(float t) =>
            t < 0.5f
                ? (float)((1 - Math.Sqrt(1 - 4 * t * t)) / 2)
                : (float)((Math.Sqrt(1 - 4 * (t - 1) * (t - 1)) + 1) / 2);

        public static float EaseInBack(float t)
        {
            float s = 1.70158f;
            return t * t * ((s + 1) * t - s);
        }

        public static float EaseOutBack(float t)
        {
            float s = 1.70158f;
            return (t -= 1) * t * ((s + 1) * t + s) + 1;
        }

        public static float EaseInOutBack(float t)
        {
            float s = 1.70158f * 1.525f;
            return t < 0.5f ? (t *= 2) * t * ((s + 1) * t - s) / 2 : ((t -= 2) * t * ((s + 1) * t + s) + 2) / 2;
        }

        public static float EaseInElastic(float t) =>
            (float)(Math.Sin(13 * Math.PI / 2 * t) * Math.Pow(2, 10 * (t - 1)));

        public static float EaseOutElastic(float t) =>
            (float)(Math.Sin(-13 * Math.PI / 2 * (t + 1)) * Math.Pow(2, -10 * t) + 1);

        public static float EaseInOutElastic(float t) =>
            t < 0.5f
                ? (float)(0.5f * Math.Sin(13 * Math.PI / 2 * (2 * t)) * Math.Pow(2, 10 * (2 * t - 1)))
                : (float)(0.5f * (Math.Sin(-13 * Math.PI / 2 * ((2 * t - 1) + 1)) * Math.Pow(2, -10 * (2 * t - 1)) +
                                  2));

        public static float EaseInBounce(float t) =>
            1 - EaseOutBounce(1 - t);

        public static float EaseOutBounce(float t)
        {
            if (t < (1 / 2.75f))
            {
                return 7.5625f * t * t;
            }
            else if (t < (2 / 2.75f))
            {
                return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
            }
            else if (t < (2.5f / 2.75f))
            {
                return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
            }
            else
            {
                return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
            }
        }

        public static float EaseInOutBounce(float t) =>
            t < 0.5f ? EaseInBounce(t * 2) * 0.5f : EaseOutBounce(t * 2 - 1) * 0.5f + 0.5f;
    }
}