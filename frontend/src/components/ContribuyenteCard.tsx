
import { Building2, User } from "lucide-react"
import type { Contribuyente } from "../types"

interface ContribuyenteCardProps {
    contribuyente: Contribuyente
    isSelected: boolean
    onClick: () => void
}

export default function ContribuyenteCard({ contribuyente, isSelected, onClick }: ContribuyenteCardProps) {
    return (
        <div
            className={`p-4 border-b border-gray-100 cursor-pointer hover:bg-gray-50 transition-colors ${isSelected ? "bg-green-50 border-l-4 border-l-green-500" : ""
                }`}
            onClick={onClick}
        >
            <div className="flex items-start space-x-3">
                <div className="flex-shrink-0">
                    {contribuyente.tipo === "PERSONA FISICA" ? (
                        <User className="h-5 w-5 text-gray-500" />
                    ) : (
                        <Building2 className="h-5 w-5 text-gray-500" />
                    )}
                </div>
                <div className="flex-1 min-w-0">
                    <p className="text-sm font-medium text-gray-900 truncate">{contribuyente.nombre}</p>
                    <p className="text-sm text-gray-500">RNC/Cédula: {contribuyente.rncCedula}</p>
                    <div className="flex items-center space-x-2 mt-1">
                        <span className="text-xs text-gray-500">{contribuyente.tipo}</span>
                        <span
                            className={`inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium ${contribuyente.estatus === "activo" ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800"
                                }`}
                        >
                            {contribuyente.estatus}
                        </span>
                    </div>
                </div>
            </div>
        </div>
    )
}
