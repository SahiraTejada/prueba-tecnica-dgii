import { Calculator } from "lucide-react"

interface ITBISTotalProps {
    total: number
}

export default function ITBISTotal({ total }: ITBISTotalProps) {
    return (
        <div className="p-6 bg-green-50 border-t border-green-200">
            <div className="flex items-center justify-between">
                <div className="flex items-center space-x-2">
                    <Calculator className="h-5 w-5 text-green-600" />
                    <span className="text-lg font-semibold text-gray-800">Total ITBIS:</span>
                </div>
                <span className="text-2xl font-bold text-green-600">${total.toFixed(2)}</span>
            </div>
            <p className="text-sm text-gray-600 mt-2">Suma total del ITBIS de todos los comprobantes fiscales</p>
        </div>
    )
}
