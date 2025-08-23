
import { Building2 } from "lucide-react"
import type { Contribuyente } from "../types"
import SearchBar from "./SearchBar"
import ContribuyenteCard from "./ContribuyenteCard"

interface ContribuyentesListProps {
    contribuyentes: Contribuyente[]
    selectedContribuyente: Contribuyente | null
    searchTerm: string
    onSearchChange: (term: string) => void
    onContribuyenteSelect: (contribuyente: Contribuyente) => void
}

export default function ContribuyentesList({
    contribuyentes,
    selectedContribuyente,
    searchTerm,
    onSearchChange,
    onContribuyenteSelect,
}: ContribuyentesListProps) {
    const filteredContribuyentes = contribuyentes.filter(
        (contribuyente) =>
            contribuyente.nombre.toLowerCase().includes(searchTerm.toLowerCase()) ||
            contribuyente.rncCedula.includes(searchTerm),
    )

    return (
        <div className="bg-white rounded-lg shadow-md">
            <div className="p-6 border-b border-gray-200">
                <div className="flex items-center space-x-2 mb-4">
                    <Building2 className="h-6 w-6 text-green-600" />
                    <h2 className="text-xl font-semibold text-gray-800">Lista de Contribuyentes</h2>
                </div>
                <SearchBar searchTerm={searchTerm} onSearchChange={onSearchChange} />
            </div>

            <div className="max-h-96 overflow-y-auto">
                {filteredContribuyentes.map((contribuyente) => (
                    <ContribuyenteCard
                        key={contribuyente.rncCedula}
                        contribuyente={contribuyente}
                        isSelected={selectedContribuyente?.rncCedula === contribuyente.rncCedula}
                        onClick={() => onContribuyenteSelect(contribuyente)}
                    />
                ))}
            </div>
        </div>
    )
}
