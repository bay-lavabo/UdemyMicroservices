using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.Core.Base.Concrete
{
    public abstract class EntityBase
    {
        private int? _requestedHashCode;
        private int _Id;
        
        public virtual int Id
        {
            get => _Id;
            set => _Id = value;
        }

        //Default değer varsa geçici mi değil mi? Defaultsa veritabanında karşılığı yok anlamına gelir.
        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        //Equal metodunu override ediyor iki objenin birbirine eşit olup olmadığına bakıyor. Tip referans ve id kontrol ediyor.
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            EntityBase item = (EntityBase)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        //Soldaki ve sağdaki nesneler eşit mi?
        public static bool operator ==(EntityBase left, EntityBase right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        //Soldaki ve sağdaki nesneler eşit değil mi?
        public static bool operator !=(EntityBase left, EntityBase right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }


    }
}
