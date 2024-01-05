var $imageupload = $('.imageupload');
var mainPhotoURL = $("#MainPhotoURL").val();
$imageupload.imageupload({ imgSrc: mainPhotoURL });

$('#imageupload-disable').on('click', function () {
    $imageupload.imageupload('disable');
    $(this).blur();
})

$('#imageupload-enable').on('click', function () {
    $imageupload.imageupload('enable');
    $(this).blur();
})

$('#imageupload-reset').on('click', function () {
    $imageupload.imageupload('reset');
    $(this).blur();
});


$(document).ready(function () {
    var photoCount = 1; 

    function gatherFormData() {

        var formData = new FormData($("#productForm")[0]);

        var mainPhotoInput = document.getElementById("MainPhotoInput");
        var mainPhotoFile = mainPhotoInput.files;

        formData.append("MainPhoto", mainPhotoFile, mainPhotoFile.name);
        formData.append("Name", $("#Name").val());
        formData.append("Description", $("#Description").val());
        formData.append("Price", $("#Price").val());
        formData.append("StockQuantity", $("#StockQuantity").val());
        formData.append("CategoryID", $("#CategoryID").val()); 

        for (var i = 0; i < otherPhotos.length; i++) {
            formData.append("OtherPhotos", otherPhotos[i]);
        }
        console.log(formData);
        return formData;
    }

    function handleFileChange(event) {
        var file = event.target.files[0];
        otherPhotos.push(file);
    }

    $("#submit-button").click(function () {
        var formData = gatherFormData();

        $.ajax({
            url: '/Product/Update',
            type: 'POST',
            processData: false,
            contentType: false,
            data: formData,
            success: function (result) {
               
            },
            error: function (error) {
              
            }
        });
    });

    const otherPhotosUrls = document.querySelectorAll("#otherPhotosURLID");
    console.log(otherPhotosUrls)
    otherPhotosUrls.forEach((url, index) => {
        addPhotoFieldWithURL(url.value)
    });

    function addPhotoFieldWithURL(imageURL) {
        var newField = `
                    <div class="imageupload panel panel-default mb-3 mr-3" id="panel-${photoCount}">
                        <div class="panel-heading clearfix">
                            <label for="photos">Ek Fotoğraf ${photoCount}:</label>
                        </div>
                        <div class="file-tab panel-body">
                                <div class="d-flex justify-content-between">
                            <label class="btn btn-primary btn-file  m-0" style="width:45%;">
                                <i></i>
                                <!-- The file is stored here. -->
                           <input name="OtherPhotos" type="file" id="image-file-${photoCount}" onchange="handleFileChange(event)">
                            </label>
                                    <button type="button" class="btn btn-primary" style="width:45%;">X</button>
                        </div>
 
                        </div>
                    </div>
                `;

        $("#additional-photo-fields").append(newField);

        $(`#panel-${photoCount}`).imageupload({ imgSrc: imageURL });

        photoCount++;
    }

    function addPhotoField() {
        var newField = `
                    <div class="imageupload panel panel-default mb-3 mr-3" id="panel-${photoCount}">
                        <div class="panel-heading clearfix">
                            <label for="photos">Ek Fotoğraf ${photoCount}:</label>
                        </div>
                        <div class="file-tab panel-body">
                                <div class="d-flex justify-content-between">
                            <label class="btn btn-primary btn-file  m-0" style="width:45%;">
                                <i></i>
                                <!-- The file is stored here. -->
                           <input name="OtherPhotos" type="file" id="image-file-${photoCount}" onchange="handleFileChange(event)">
                            </label>
                                    <button type="button" class="btn btn-primary" style="width:45%;">X</button>
                        </div>
 
                        </div>
                    </div>
                `;

        $("#additional-photo-fields").append(newField);
        
        $(`#panel-${photoCount}`).imageupload({ imgSrc: "http://www.gstatic.com/webp/gallery/5.jpg" });

        photoCount++;
    }
    
    function removePhotoField(button) {
        $(button).closest('.imageupload').remove();
    }
    
    function disablePhotoUpload(count) {
        $(`#panel-${count}`).imageupload('disable');
    }
    
    function enablePhotoUpload(count) {
        $(`#panel-${count}`).imageupload('enable');
    }
    
    function resetPhotoUpload(count) {
        $(`#panel-${count}`).imageupload('reset');
    }
    
    $("#add-photo-field").click(function () {
        addPhotoField();
    });

    $("#remove-last-field").click(function () {
        $("#additional-photo-fields .imageupload:last-child").remove();
        photoCount--;
    });


});