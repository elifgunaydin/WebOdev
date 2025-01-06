async function getCalisanlar() {
    const hizmetID = document.getElementById("hizmet").value;
    const calisanSelect = document.getElementById("calisan");

    // Çalışan dropdown'ı temizlenir
    calisanSelect.innerHTML = '<option value="">Yükleniyor...</option>';

    if (!hizmetID) {
        calisanSelect.innerHTML = '<option value="">Önce bir hizmet seçin</option>';
        return;
    }

    try {
        // Backend'den çalışanları AJAX ile çek
        const response = await fetch(`/Randevu/GetCalisanlar?hizmetID=${hizmetID}`);
        if (!response.ok) throw new Error("Çalışan bilgisi alınamadı.");

        const calisanlar = await response.json();

        // Çalışan dropdown'ını güncelle
        calisanSelect.innerHTML = '<option value="">Çalışan Seçin</option>';
        calisanlar.forEach(calisan => {
            const option = document.createElement("option");
            option.value = calisan.calisanID;
            option.textContent = `${calisan.ad} ${calisan.soyad}`;
            calisanSelect.appendChild(option);
        });
    } catch (error) {
        console.error(error);
        calisanSelect.innerHTML = '<option value="">Hata oluştu</option>';
    }
}
