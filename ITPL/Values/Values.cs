using System;

using Positions;

namespace Types
{
    public class ObjectBase
    {
        public dynamic value;
        public Position start;
        public Position end;

        public ObjectBase(dynamic value)
        {
            this.value = value;
        }

        public void SetPosition(Position start, Position end)
        {
            if(start != null) this.start = start;
            if(end != null) this.end = end;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        public ObjectBase AddTo(ObjectBase other)
        {
            return  new ObjectBase(this.value + other.value);
        }

        public ObjectBase SubBy(ObjectBase other)
        {
            return  new ObjectBase(this.value - other.value);
        }

        public ObjectBase MulBy(ObjectBase other)
        {
            return  new ObjectBase(this.value * other.value);
        }

        public ObjectBase DivBy(ObjectBase other)
        {
            return  new ObjectBase(this.value / other.value);
        }
    }
    
    public class Number : ObjectBase
    {
        public Number(int value) : base(value)
        {

        }

        public Number(double value) : base(value)
        {

        }
    }

    public class Integer : Number
    {
        public Integer(int value) : base(value)
        {
            this.value = value;
        }
        
        public Integer AddTo(Number other)
        {
            return new Integer(this.value + other.value);
        }

        public Integer SubBy(Number other)
        {
            return new Integer(this.value - other.value);
        }

        public Integer MulBy(Number other)
        {
            return new Integer(this.value * other.value);
        }

        public Integer DivBy(Number other)
        {
            return new Integer(this.value / other.value);
        }
    }

    public class DecimalValue : Number
    {
        public DecimalValue(double value) : base(value)
        {
            this.value = value;
        }

        public DecimalValue AddTo(Number other)
        {
            return new DecimalValue(this.value + other.value);
        }

        public DecimalValue SubBy(Number other)
        {
            return new DecimalValue(this.value - other.value);
        }

        public DecimalValue MulBy(Number other)
        {
            return new DecimalValue(this.value * other.value);
        }

        public DecimalValue DivBy(Number other)
        {
            return new DecimalValue(this.value / other.value);
        }
    }
}
