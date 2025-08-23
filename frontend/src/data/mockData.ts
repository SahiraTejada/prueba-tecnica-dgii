import type { Contribuyente, ComprobanteFiscal } from "../types"

export const contribuyentes: Contribuyente[] = [
    {
        rncCedula: "98754321012",
        nombre: "JUAN PEREZ",
        tipo: "PERSONA FISICA",
        estatus: "activo",
    },
    {
        rncCedula: "123456789",
        nombre: "FARMACIA TU SALUD",
        tipo: "PERSONA JURIDICA",
        estatus: "inactivo",
    },
    {
        rncCedula: "987654321",
        nombre: "MARIA RODRIGUEZ",
        tipo: "PERSONA FISICA",
        estatus: "activo",
    },
    {
        rncCedula: "456789123",
        nombre: "SUPERMERCADO LA ECONOMIA",
        tipo: "PERSONA JURIDICA",
        estatus: "activo",
    },
    {
        rncCedula: "789123456",
        nombre: "CARLOS MARTINEZ",
        tipo: "PERSONA FISICA",
        estatus: "inactivo",
    },
]

export const comprobantesFiscales: ComprobanteFiscal[] = [
    {
        rncCedula: "98754321012",
        NCF: "E310000000001",
        monto: "200.00",
        itbis18: "36.00",
    },
    {
        rncCedula: "98754321012",
        NCF: "E310000000002",
        monto: "1000.00",
        itbis18: "180.00",
    },
    {
        rncCedula: "123456789",
        NCF: "E310000000003",
        monto: "500.00",
        itbis18: "90.00",
    },
    {
        rncCedula: "987654321",
        NCF: "E310000000004",
        monto: "750.00",
        itbis18: "135.00",
    },
    {
        rncCedula: "456789123",
        NCF: "E310000000005",
        monto: "1200.00",
        itbis18: "216.00",
    },
    {
        rncCedula: "456789123",
        NCF: "E310000000006",
        monto: "800.00",
        itbis18: "144.00",
    },
]
