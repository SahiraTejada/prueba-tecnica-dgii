import { useState } from 'react'
import Header from './components/Header'
import ComprobantesPanel from './components/ComprobantesPanel'
import ContribuyentesList from './components/ContribuyentesList'
import { contribuyentes, comprobantesFiscales } from './data/mockData'
import type { Contribuyente } from './types'


function App() {
    const [selectedContribuyente, setSelectedContribuyente] = useState<Contribuyente | null>(null)
    const [searchTerm, setSearchTerm] = useState("")
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
                      onContribuyenteSelect={setSelectedContribuyente}
                  />

                  <ComprobantesPanel selectedContribuyente={selectedContribuyente} comprobantes={comprobantesFiscales} />
              </div>
          </div>
      </div>
  )
}

export default App
