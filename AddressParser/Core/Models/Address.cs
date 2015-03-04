#region usings
using System;
using System.Data.SqlClient;

#endregion



namespace CE.Parsing.Core.Models
{
    internal class Address
    {
        int? _addressId;


        public int? AddressId
        {
            get { return _addressId; }
            set
            {
                ClearIds();
                Room = null;
                _addressId = value;
                HierarchyLevel++;
            }
        }


        public Guid? AoId { get; private set; }
        public Guid? LandMarkId { get; private set; }

        public Guid? HouseId { get; private set; }

        public Guid? AddonAoId { get; private set; }

        public Guid? AddonHouseId { get; private set; }

        public string Room;
        public bool? IsAllWordsFound;
        public int HierarchyLevel { get; private set; }

        public AddrObject AddrObject { get; private set; }
        public AddrHouse AddrHouse { get; private set; }


        public Address(AddrObject ao)
        {
            SetAddrObject(ao);
        }


        public Address(SqlDataReader reader)
        {
            AoId = reader.IsDBNull(0) ? (Guid?) null : reader.GetGuid(0);
            LandMarkId = reader.IsDBNull(1) ? (Guid?) null : reader.GetGuid(1);
            HouseId = reader.IsDBNull(2) ? (Guid?) null : reader.GetGuid(2);
            AddonAoId = reader.IsDBNull(3) ? (Guid?) null : reader.GetGuid(3);
            AddonHouseId = reader.IsDBNull(4) ? (Guid?) null : reader.GetGuid(4);
            Room = reader.IsDBNull(5) ? null : reader.GetString(5);
        }


        void ClearIds()
        {
            AoId = null;
            LandMarkId = null;
            AddonAoId = null;
            HouseId = null;
            AddonHouseId = null;
        }


        public void SetAddrObject(AddrObject addrObject)
        {
            AddrHouse = null;
            AddrObject = addrObject;
            ClearIds();

            if (addrObject.Kind == AddrObjectKind.AddrObjectCurrent)
                AoId = addrObject.Id;
            else if (addrObject.Kind == AddrObjectKind.AddrLandMarkCurrent)
                LandMarkId = addrObject.Id;
            else if (addrObject.Kind == AddrObjectKind.AddonAddrObject)
                AddonAoId = addrObject.Id;

            HierarchyLevel = addrObject.HierarchyLevel;
        }


        public void SetHouse(AddrHouse ah)
        {
            AddrHouse = ah;
            ClearIds();

            if (ah.IsAddon)
                AddonHouseId = ah.Id;
            else
                HouseId = ah.Id;

            HierarchyLevel++;
        }


        public override string ToString()
        {
            return string.Format("AddressId: {0}, AoId: {1}, LandMarkId: {2}, HouseId: {3}, AddonAoId: {4}, AddonHouseId: {5}, HierarchyLevel: {6}, IsAllWordsFound: {7}", AddressId, AoId, LandMarkId, HouseId, AddonAoId,
                AddonHouseId, HierarchyLevel, IsAllWordsFound);
        }
    }
}