class FileUploader
{
    _imgInputId;
    _imgValueId;
    _imgDisplayTagId;

    constructor(imgInputId, imgValueId, imgDisplayTagId){
        this._imgInputId = !imgInputId ? 'File' : imgInputId;
        this._imgValueId = !imgValueId ? 'FileUrl' : imgValueId;
        this._imgDisplayTagId = !imgDisplayTagId ? 'FileDisplay' : imgDisplayTagId;
        
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
                alert('Failed to upload image.');
            }
        })
        .catch(error => console.error('Error uploading file:', error));
    };
    
    fetchImage(self, fileName) {
        fetch(`/File/SignedURL?fileName=${encodeURIComponent(fileName)}`)
        .then(response => response.json())
        .then(data => {
            if (data.signedUrl) {
                var uploadedImage = document.getElementById(self._imgDisplayTagId);
                uploadedImage.src = data.signedUrl;
                uploadedImage.style.display = 'block'; // Show the image element
            } else {
                alert('Failed to retrieve image.');
            }
        })
        .catch(error => {
            console.error('Error fetching file:', error);
            alert('Error fetching file.');
        });
    }
}