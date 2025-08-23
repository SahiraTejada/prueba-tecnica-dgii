export interface Contribuyente {
    rncCedula: string
    nombre: string
    tipo: "PERSONA FISICA" | "PERSONA JURIDICA"
    estatus: "activo" | "inactivo"
}

export interface ComprobanteFiscal {
    rncCedula: string
    NCF: string
    monto: string
    itbis18: string
}
