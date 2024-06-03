class FileUploader
{
    _imgInputId;
    _imgValueId;
    _imgDisplayTagId;
    _maxSizeMB;

    constructor(imgInputId, imgValueId, imgDisplayTagId, maxSizeMB){
        this._imgInputId = !imgInputId ? 'File' : imgInputId;
        this._imgValueId = !imgValueId ? 'FileUrl' : imgValueId;
        this._imgDisplayTagId = !imgDisplayTagId ? 'FileDisplay' : imgDisplayTagId;
        this._maxSizeMB = maxSizeMB;

        this.initializeListener();
        this.initializeImageDisplay();
    }

    initializeImageDisplay(){
        const imgVlaueElement = document.getElementById(this._imgValueId);
        if(imgVlaueElement.value)
        {
            this.fetchImage(this, imgVlaueElement.value);
        }
    }

    initializeListener(){
        document.getElementById(this._imgInputId).addEventListener('change', (event) =>
        {
            this.uploadImage(this, event)
        });
    }

    uploadImage(self, event) {
        ShowLoadingModal();

        if (self._maxSizeMB) {
            // Check file max size
            if (event.target.files.length > 0 && event.target.files[0].size > self._maxSizeMB * 1000000) {
                swal.close();
                Swal.fire({
                    title: "La imagen es demasiado grande",
                    text: `El tama\u00F1o m\xe1ximo permitido es de ${self._maxSizeMB} MB.`,
                    type: "error"
                });
                return;
            }
        }

        var formData = new FormData();
        formData.append('file', event.target.files[0]);
    
        fetch('/File/Upload', {
            method: 'POST',
            body: formData,
        })
        .then(response => response.json())
        .then(data => {
            if (data.fileUrl) {
                document.getElementById('FileUrl').value = data.fileUrl;
                // Now fetch the display URL from the download endpoint
                self.fetchImage(self, data.fileUrl, self._imgDisplayTagId);
            } else {
                swal.close();
                alert('Error al subir la imagen.');
            }
        })
        .catch(error => {
            swal.close();
            console.error('Error al subir el archivo:', error)
        });
    };
    
    fetchImage(self, fileName) {
        fetch(`/File/PublicURL?fileName=${encodeURIComponent(fileName)}`)
        .then(response => response.json())
        .then(data => {
            if (data.publicUrl) {
                var uploadedImage = document.getElementById(self._imgDisplayTagId);
                uploadedImage.src = data.publicUrl;
                uploadedImage.style.display = 'block'; // Show the image element
                swal.close();
            } else {
                swal.close();
                alert('Error al obtener la imagen.');
            }
        })
        .catch(error => {
            swal.close();
            console.error('Error buscando el archivo:', error);
            alert('Error buscando el archivo.');
        });
    }
}