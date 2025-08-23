import type { ComprobanteFiscal } from "../types"

interface ComprobanteCardProps {
    comprobante: ComprobanteFiscal
}

export default function ComprobanteCard({ comprobante }: ComprobanteCardProps) {
    return (
        <div className="p-4 border-b border-gray-100 hover:bg-gray-50">
            <div className="flex justify-between items-start">
                <div>
                    <p className="text-sm font-medium text-gray-900">NCF: {comprobante.NCF}</p>
                    <p className="text-sm text-gray-600">Monto: ${Number.parseFloat(comprobante.monto).toFixed(2)}</p>
                </div>
                <div className="text-right">
                    <p className="text-sm font-medium text-green-600">
                        ITBIS: ${Number.parseFloat(comprobante.itbis18).toFixed(2)}
                    </p>
                </div>
            </div>
        </div>
    )
}
