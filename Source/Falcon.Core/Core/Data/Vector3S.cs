
using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;

namespace Falcon.Core.Data
{
    /// <summary>
    ///     Represents a 3-dimensional vector data structure using <see cref="short" />s as value types.
    /// </summary>
    public struct Vector3S : IEquatable<Vector3S>, IFormattable
    {
        #region Fields

        public static readonly Vector3S Zero = new Vector3S(0, 0, 0);

        public static readonly Vector3S One = new Vector3S(1, 1, 1);

        public short X;

        public short Y;

        public short Z;

        #endregion

        #region Constructors

        public Vector3S(short x, short y, short z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Methods

        public static void Add(ref Vector3S left, ref Vector3S right, out Vector3S result)
        {
            result = new Vector3S((short) (left.X + right.X), (short) (left.Y + right.Y), (short) (left.Z + right.Z));
        }

        public static Vector3S Add(Vector3S left, Vector3S right)
        {
            return new Vector3S((short) (left.X + right.X), (short) (left.Y + right.Y), (short) (left.Z + right.Z));
        }

        public static void Subtract(ref Vector3S left, ref Vector3S right, out Vector3S result)
        {
            result = new Vector3S((short) (left.X - right.X), (short) (left.Y - right.Y), (short) (left.Z - right.Z));
        }

        public static Vector3S Subtract(Vector3S left, Vector3S right)
        {
            return new Vector3S((short) (left.X - right.X), (short) (left.Y - right.Y), (short) (left.Z - right.Z));
        }

        public static void Multiply(ref Vector3S value, short scale, out Vector3S result)
        {
            result = new Vector3S((short) (value.X * scale), (short) (value.Y * scale), (short) (value.Z * scale));
        }

        public static Vector3S Multiply(Vector3S value, short scale)
        {
            return new Vector3S((short) (value.X * scale), (short) (value.Y * scale), (short) (value.Z * scale));
        }

        public static void Divide(ref Vector3S value, short scale, out Vector3S result)
        {
            result = new Vector3S((short) (value.X / scale), (short) (value.Y / scale), (short) (value.Z / scale));
        }

        public static Vector3S Divide(Vector3S value, short scale)
        {
            return new Vector3S((short) (value.X / scale), (short) (value.Y / scale), (short) (value.Z / scale));
        }

        public static void Negate(Vector3S value, out Vector3S result)
        {
            result = new Vector3S((short) -value.X, (short) -value.Y, (short) -value.Z);
        }

        public static Vector3S Negate(Vector3S value)
        {
            return new Vector3S((short) -value.X, (short) -value.Y, (short) -value.Z);
        }

        public static void Abs(ref Vector3S value, out Vector3S result)
        {
            result = new Vector3S
            (
                (short) (value.X > 0 ? value.X : -value.X),
                (short) (value.Y > 0 ? value.Y : -value.Y),
                (short) (value.Z > 0 ? value.Z : -value.Z)
            );
        }

        public static Vector3S Abs(Vector3S value)
        {
            return new Vector3S
            (
                (short) (value.X > 0 ? value.X : -value.X),
                (short) (value.Y > 0 ? value.Y : -value.Y),
                (short) (value.Z > 0 ? value.Z : -value.Z)
            );
        }

        public static void Clamp(ref Vector3S value, ref Vector3S min, ref Vector3S max, out Vector3S result)
        {
            short x = value.X;
            x = x > max.X ? max.X : x;
            x = x < min.X ? min.X : x;

            short y = value.Y;
            y = y > max.Y ? max.Y : y;
            y = y < min.Y ? min.Y : y;

            short z = value.Z;
            z = z > max.Z ? max.Z : z;
            z = z < min.Z ? min.Z : z;

            result = new Vector3S(x, y, z);
        }

        public static Vector3S Clamp(Vector3S value, Vector3S min, Vector3S max)
        {
            Clamp(ref value, ref min, ref max, out Vector3S result);

            return result;
        }

        public static void Cross(ref Vector3S left, ref Vector3S right, out Vector3S result)
        {
            result = new Vector3S
            (
                (short) ((short) (left.Y * right.Z) - (short) (left.Z * right.Y)),
                (short) ((short) (left.Z * right.X) - (short) (left.X * right.Z)),
                (short) ((short) (left.X * right.Y) - (short) (left.Y * right.X))
            );
        }

        public static Vector3S Cross(Vector3S left, Vector3S right)
        {
            return new Vector3S
            (
                (short) ((short) (left.Y * right.Z) - (short) (left.Z * right.Y)),
                (short) ((short) (left.Z * right.X) - (short) (left.X * right.Z)),
                (short) ((short) (left.X * right.Y) - (short) (left.Y * right.X))
            );
        }

        public static void Distance(ref Vector3S value1, ref Vector3S value2, out short result)
        {
            short x = (short) (value1.X - value2.X);
            short y = (short) (value1.Y - value2.Y);
            short z = (short) (value1.Z - value2.Z);

            result = (short) Math.Sqrt((short) (x * x) + (short) (y * y) + (short) (z * z));
        }

        public static short Distance(Vector3S value1, Vector3S value2)
        {
            short x = (short) (value1.X - value2.X);
            short y = (short) (value1.Y - value2.Y);
            short z = (short) (value1.Z - value2.Z);

            return (short) Math.Sqrt((short) (x * x) + (short) (y * y) + (short) (z * z));
        }

        public static void DistanceSquared(ref Vector3S value1, ref Vector3S value2, out short result)
        {
            short x = (short) (value1.X - value2.X);
            short y = (short) (value1.Y - value2.Y);
            short z = (short) (value1.Z - value2.Z);

            result = (short) ((short) (x * x) + (short) (y * y) + (short) (z * z));
        }

        public static short DistanceSquared(Vector3S value1, Vector3S value2)
        {
            short x = (short) (value1.X - value2.X);
            short y = (short) (value1.Y - value2.Y);
            short z = (short) (value1.Z - value2.Z);

            return (short) ((short) (x * x) + (short) (y * y) + (short) (z * z));
        }

        public static void Dot(ref Vector3S left, ref Vector3S right, out short result)
        {
            result = (short) ((short) (left.X * right.X) + (short) (left.Y * right.Y) + (short) (left.Z * right.Z));
        }

        public static short Dot(Vector3S left, Vector3S right)
        {
            return (short) ((short) (left.X * right.X) + (short) (left.Y * right.Y) + (short) (left.Z * right.Z));
        }

        public static void Normalize(ref Vector3S value, out Vector3S result)
        {
            result = value;
            result.Normalize();
        }

        public static Vector3S Normalize(Vector3S value)
        {
            value.Normalize();

            return value;
        }

        public short Length()
        {
            return (short) Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public void Normalize()
        {
            short length = Length();

            if (Math.Abs(length) < 1e-6) return; // All numbers smaller than 1e-6 are considered to be equal to zero.

            short inv = (short) (1 / length);
            X *= inv;
            Y *= inv;
            Z *= inv;
        }

        public static void Min(ref Vector3S left, ref Vector3S right, out Vector3S result)
        {
            result.X = left.X < right.X ? left.X : right.X;
            result.Y = left.Y < right.Y ? left.Y : right.Y;
            result.Z = left.Z < right.Z ? left.Z : right.Z;
        }

        public static Vector3S Min(Vector3S left, Vector3S right)
        {
            Min(ref left, ref right, out Vector3S result);

            return result;
        }

        public static void Max(ref Vector3S left, ref Vector3S right, out Vector3S result)
        {
            result.X = left.X > right.X ? left.X : right.X;
            result.Y = left.Y > right.Y ? left.Y : right.Y;
            result.Z = left.Z > right.Z ? left.Z : right.Z;
        }

        public static Vector3S Max(Vector3S left, Vector3S right)
        {
            Max(ref left, ref right, out Vector3S result);

            return result;
        }

        //public static void Project(ref Vector3S value, in short x, in short y, in short z, in short width, in short height, in short minZ, in short maxZ,
        //    ref Matrix worldViewProjection, out Vector3S result)
        //{
        //    Vector3S vectorValue = new Vector3S();

        //    TransformCoordinate(ref value, ref worldViewProjection, out vectorValue);

        //    result = new Vector3S((short) ((1 + vectorValue.X) * 0.5 * width + x), (short) ((1 - vectorValue.Y) * 0.5 * height + y), (short) (vectorValue.Z * (maxZ - minZ) + minZ));
        //}
        public short [] ToArray()
        {
            return new [] {X, Y, Z};
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return format == null ? ToString(formatProvider) : string.Format(formatProvider, format, X.ToString(format, CultureInfo.CurrentCulture), Y.ToString(format, CultureInfo.CurrentCulture), Z.ToString(format, CultureInfo.CurrentCulture));
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", X, Y, Z);
        }

        public string ToString(string format)
        {
            if (format == null)
                return ToString();

            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}",
                X.ToString(format, CultureInfo.CurrentCulture),
                Y.ToString(format, CultureInfo.CurrentCulture), Z.ToString(format, CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", X, Y, Z);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return HashCode.Combine(X, Y, Z);
            }
        }

        public bool Equals(Vector3S other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3S other && Equals(other);
        }

        #endregion

        #region Operators

        public static Vector3S operator +(Vector3S left, Vector3S right)
        {
            return new Vector3S((short) (left.X + right.X), (short) (left.Y + right.Y), (short) (left.Z + right.Z));
        }

        public static Vector3S operator -(Vector3S left, Vector3S right)
        {
            return new Vector3S((short) (left.X - right.X), (short) (left.Y - right.Y), (short) (left.Z - right.Z));
        }

        public static Vector3S operator *(Vector3S value, short scale)
        {
            return new Vector3S((short) (value.X * scale), (short) (value.Y * scale), (short) (value.Z * scale));
        }

        public static Vector3S operator /(Vector3S value, short scale)
        {
            return new Vector3S((short) (value.X / scale), (short) (value.Y / scale), (short) (value.Z / scale));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3S left, Vector3S right)
        {
            return left.Equals(right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3S left, Vector3S right)
        {
            return !left.Equals(right);
        }

        public static unsafe implicit operator Vector3(Vector3S value)
        {
            return *(Vector3*)&value;
        }

        public static unsafe implicit operator Vector3S(Vector3 value)
        {
            return *(Vector3S*) &value;
        }

        public static unsafe implicit operator Vector3D(Vector3S value)
        {
            return *(Vector3D*) &value;
        }

        public static unsafe implicit operator Vector3S(Vector3D value)
        {
            return *(Vector3S*) &value;
        }

        #endregion
    }
}