﻿using ParkingChallenge.Core.Domain.Exceptions;
using ParkingChallenge.Core.Domain.Validation;

namespace ParkingChallenge.Core.Domain.Entities;
public class Parking : Entity
{
    public Parking(Spaces carSpaces, Spaces vanSpaces, Spaces motorcyclesSpaces)
    {
        CarSpaces = carSpaces;
        VanSpaces = vanSpaces;
        MotorcyclesSpaces = motorcyclesSpaces;
        Validate();
        Initialize();
    }

    public bool CarsFull => CalculateCarSpace();

    public bool MotorcyclesFull => CalculateMotorcycleSpace();

    public bool VansFull => CalculateVanSpace();

    public int TotalSpaces => CalculateTotalSpaces();

    public int RemainingSpaces => CalculateFreeSpaces();

    public bool IsFull => RemainingSpaces == 0;

    public bool IsEmpty => TotalSpaces == RemainingSpaces;

    public Spaces CarSpaces { get; set; }


    public Spaces VanSpaces { get; set; }

    public Spaces MotorcyclesSpaces { get; set; }

    private void Validate()
    {
        ValidateTotalSpaces();
        ValidateOccupiedAndFreeSpaces();
    }

    private void ValidateTotalSpaces()
    {
        DomainValidation.NotNull(CarSpaces.Total, nameof(CarSpaces.Total));
        DomainValidation.NotNull(VanSpaces.Total, nameof(VanSpaces.Total));
        DomainValidation.NotNull(MotorcyclesSpaces.Total, nameof(MotorcyclesSpaces.Total));
    }

    private void ValidateOccupiedAndFreeSpaces()
    {
        DomainValidation.NotNull(CarSpaces.Occupied, nameof(CarSpaces.Occupied));
        DomainValidation.NotNull(VanSpaces.Occupied, nameof(VanSpaces.Occupied));
        DomainValidation.NotNull(MotorcyclesSpaces.Occupied, nameof(MotorcyclesSpaces.Occupied));
        DomainValidation.MinValue(CarSpaces.Free, 0, nameof(CarSpaces.Free));
        DomainValidation.MinValue(CarSpaces.Occupied, 0, nameof(CarSpaces.Occupied));
        DomainValidation.MinValue(MotorcyclesSpaces.Free, 0, nameof(MotorcyclesSpaces.Free));
        DomainValidation.MinValue(MotorcyclesSpaces.Occupied, 0, nameof(MotorcyclesSpaces.Occupied));
        DomainValidation.MinValue(VanSpaces.Free, 0, nameof(VanSpaces.Free));
        DomainValidation.MinValue(VanSpaces.Occupied, 0, nameof(VanSpaces.Occupied));
        DomainValidation.MinValue(CalculateTotalSpaces(), 1, nameof(TotalSpaces));

        if (CarSpaces.Total != 0 && VanSpaces.Total != 0 && MotorcyclesSpaces.Total != 0)
        {
            return;
        }
        throw new EntityValidationException("All types of vehicle spaces must be greater than zero.");
    }

    private int CalculateTotalSpaces()
    {
        int carTotal = Math.Max(0, CarSpaces.Total);
        int vanTotal = Math.Max(0, VanSpaces.Total);
        int motorcycleTotal = Math.Max(0, MotorcyclesSpaces.Total);

        return carTotal + vanTotal + motorcycleTotal;
    }

    private int CalculateFreeSpaces()
        => CarSpaces.Free + VanSpaces.Free + MotorcyclesSpaces.Free;

    private bool CalculateCarSpace()
        => CarSpaces.Occupied == CarSpaces.Total;

    private bool CalculateMotorcycleSpace()
        => MotorcyclesSpaces.Occupied == MotorcyclesSpaces.Total;
    private bool CalculateVanSpace()
    => VanSpaces.Occupied == VanSpaces.Total;

    public bool CanVanPark()
        => VanSpaces.Occupied * 3 <= CarSpaces.Free || (VanSpaces.Occupied == 1 && CarSpaces.Free >= 1);

    public void ParkVan()
    {
        if (!CanVanPark())
        {
            return;
        }

        int spacesToDeduct = VanSpaces.Occupied == 1 ? 3 : VanSpaces.Occupied * 3;
        CarSpaces.Free -= spacesToDeduct;

        if (VanSpaces.Occupied < VanSpaces.Total)
        {
            VanSpaces.Occupied++;
        }
    }
}

