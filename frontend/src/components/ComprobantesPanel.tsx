import { FileText } from "lucide-react"
import type { Contribuyente, ComprobanteFiscal } from "../types"
import ComprobanteCard from "./ComprobanteCard"
import ITBISTotal from "./ITBISTotal"

interface ComprobantesPanelProps {
    selectedContribuyente: Contribuyente | null
    comprobantes: ComprobanteFiscal[]
}

export default function ComprobantesPanel({ selectedContribuyente, comprobantes }: ComprobantesPanelProps) {
    const getComprobantesByContribuyente = (rncCedula: string) => {
        return comprobantes.filter((comprobante) => comprobante.rncCedula === rncCedula)
    }

    const calculateTotalITBIS = (rncCedula: string) => {
        const contribuyenteComprobantes = getComprobantesByContribuyente(rncCedula)
        return contribuyenteComprobantes.reduce((total, comprobante) => total + Number.parseFloat(comprobante.itbis18), 0)
    }

    return (
        <div className="bg-white rounded-lg shadow-md">
            <div className="p-6 border-b border-gray-200">
                <div className="flex items-center space-x-2">
                    <FileText className="h-6 w-6 text-green-600" />
                    <h2 className="text-xl font-semibold text-gray-800">Comprobantes Fiscales</h2>
                </div>
                {selectedContribuyente && (
                    <p className="text-sm text-gray-600 mt-2">
                        {selectedContribuyente.nombre} - {selectedContribuyente.rncCedula}
                    </p>
                )}
            </div>

            {selectedContribuyente ? (
                <div>
                    <div className="max-h-80 overflow-y-auto">
                        {getComprobantesByContribuyente(selectedContribuyente.rncCedula).map((comprobante) => (
                            <ComprobanteCard key={comprobante.NCF} comprobante={comprobante} />
                        ))}
                    </div>
                    <ITBISTotal total={calculateTotalITBIS(selectedContribuyente.rncCedula)} />
                </div>
            ) : (
                <div className="p-12 text-center">
                    <FileText className="h-12 w-12 text-gray-300 mx-auto mb-4" />
                    <p className="text-gray-500">Selecciona un contribuyente para ver sus comprobantes fiscales</p>
                </div>
            )}
        </div>
    )
}
