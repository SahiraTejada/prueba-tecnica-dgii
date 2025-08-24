import { useState, useEffect } from 'react'
import Header from './components/Header'
import ComprobantesPanel from './components/ComprobantesPanel'
import ContribuyentesList from './components/ContribuyentesList'
import type { Contribuyente, ComprobanteFiscal } from './types'
import { fetchComprobantesFiscales, fetchContribuyenteDetalle, fetchContribuyentes } from './api/dgiiApi'


function App() {
    const [contribuyentes, setContribuyentes] = useState<Contribuyente[]>([])
    const [comprobantesFiscales, setComprobantesFiscales] = useState<ComprobanteFiscal[]>([])
    const [selectedContribuyente, setSelectedContribuyente] = useState<Contribuyente | null>(null)
    const [searchTerm, setSearchTerm] = useState("")
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState<string | null>(null)

    useEffect(() => {
        const loadData = async () => {
            try {
                setLoading(true)
                setError(null)

                const [contribuyentesData, comprobantesData] = await Promise.all([
                    fetchContribuyentes(),
                    fetchComprobantesFiscales()
                ])

                setContribuyentes(contribuyentesData)
                setComprobantesFiscales(comprobantesData)
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Error desconocido')
                console.error('Error loading data:', err)
            } finally {
                setLoading(false)
            }
        }

        loadData()
    }, [])

    const handleContribuyenteSelect = async (contribuyente: Contribuyente) => {
        setSelectedContribuyente(contribuyente)

        try {
            await fetchContribuyenteDetalle(contribuyente.rncCedula)
        } catch (err) {
            console.error('Error al cargar detalle:', err)
        }
    }

    if (loading) {
        return (
            <div className="min-h-screen bg-gray-50">
                <Header />
                <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                    <div className="flex items-center justify-center h-64">
                        <div className="text-center">
                            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-green-600 mx-auto mb-4"></div>
                            <p className="text-gray-600">Cargando datos...</p>
                        </div>
                    </div>
                </div>
            </div>
        )
    }

    if (error) {
        return (
            <div className="min-h-screen bg-gray-50">
                <Header />
                <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                    <div className="bg-red-50 border border-red-200 rounded-lg p-6 text-center">
                        <p className="text-red-600 font-medium">Error al cargar los datos</p>
                        <p className="text-red-500 text-sm mt-2">{error}</p>
                        <button
                            onClick={() => window.location.reload()}
                            className="mt-4 px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700 transition-colors"
                        >
                            Reintentar
                        </button>
                    </div>
                </div>
            </div>
        )
    }

    return (
        <div className="min-h-screen bg-gray-50">
            <Header />

            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
                    <ContribuyentesList
                        contribuyentes={contribuyentes}
                        selectedContribuyente={selectedContribuyente}
                        searchTerm={searchTerm}
                        onSearchChange={setSearchTerm}
                        onContribuyenteSelect={handleContribuyenteSelect}
                    />

                    <ComprobantesPanel
                        selectedContribuyente={selectedContribuyente}
                        comprobantes={comprobantesFiscales}
                    />
                </div>
            </div>
        </div>
    )
}

export default App