import Logo from '/public/images/dgii-logo.png'

export default function Header() {
    return (
        <header className="bg-white shadow-sm border-b-4 border-green-500">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex items-center justify-between h-16">

                    <div className="flex items-center space-x-4">
                        <img src={Logo} alt="DGII Logo" width={120} height={40} className="h-10 w-auto" />
                        <div className="hidden md:block">
                            <h1 className="text-xl font-bold text-gray-800">Sistema de Gesti&#243;n de Contribuyentes</h1>
                            <p className="text-sm text-gray-600">Direcci&#243;n General de Impuestos Internos</p>
                        </div>
                    </div>
                </div>
            </div>
        </header>
    )
}
