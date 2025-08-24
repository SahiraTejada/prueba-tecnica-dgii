const API_BASE = "http://localhost:5000/api";

export async function fetchContribuyentes() {
    const res = await fetch(`${API_BASE}/contribuyentes`);
    if (!res.ok) throw new Error("Error al obtener contribuyentes");
    return await res.json();
}

export async function fetchContribuyenteDetalle(rncCedula: string) {
    const res = await fetch(`${API_BASE}/contribuyentes/${rncCedula}`);
    if (!res.ok) throw new Error("Error al obtener detalle");
    return await res.json();
}

export async function fetchComprobantesFiscales() {
    const res = await fetch(`${API_BASE}/comprobantesfiscales`);
    if (!res.ok) throw new Error("Error al obtener comprobantes fiscales");
    return await res.json();
}