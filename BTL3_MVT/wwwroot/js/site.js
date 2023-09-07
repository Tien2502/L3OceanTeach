$(document).ready(function () {
    $('#DistrictId').select2({
        placeholder: '-- Chọn Quận, Huyện --',
    });
    $('#WardId').select2({
        placeholder: '-- Chọn Xã, Phường --',
    });
    $('#CityId').select2({
        ajax: {
            url: '/api/city/',
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.cityId,
                        text: item.cityName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn Tỉnh, Thành phố --',
        minimumInputLength: 0
    });
    $('#CityId').on('select2:select', function () {
        var cityId = $(this).val();
        $('#DistrictId').empty();
        $('#DistrictId').select2({
            ajax: {
                url: '/api/district/ByCity/' + cityId,
                type: 'GET',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        searchQuery: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, params) {
                    var results = [];
                    $.each(data, function (index, item) {
                        results.push({
                            id: item.districtId,
                            text: item.districtName
                        });
                    });

                    return {
                        results: results,
                        pagination: {
                            more: data.length >= 5
                        }
                    };
                },
                cache: true
            },
            placeholder: '-- Chọn Quận, Huyện --',
            minimumInputLength: 0
        });
        $('#WardId').empty(); // Xoá giá trị của trường #WardId khi thay đổi cityId
    });

    $('#DistrictId').on('select2:select', function () {
        var districtId = $(this).val();
        $('#WardId').empty();
        $('#WardId').select2({
            ajax: {
                url: '/api/Ward/ByDistrict/' + districtId,
                type: 'GET',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        searchQuery: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, params) {
                    var results = [];
                    $.each(data, function (index, item) {
                        results.push({
                            id: item.wardId,
                            text: item.wardName
                        });
                    });

                    return {
                        results: results,
                        pagination: {
                            more: data.length >= 5
                        }
                    };
                },
                cache: true
            },
            placeholder: '-- Chọn Xã, Phường --',
            minimumInputLength: 0
        });
    });
});

$(document).ready(function () {
    $('#WorkDistrictId').select2({
        placeholder: '-- Chọn Quận, Huyện --',
    });
    $('#WorkWardId').select2({
        placeholder: '-- Chọn Xã, Phường --',
    });
    $('#WorkCityId').select2({
        ajax: {
            url: '/api/city/',
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.cityId,
                        text: item.cityName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn Tỉnh, Thành phố --',
        minimumInputLength: 0
    });

    $('#WorkCityId').on('select2:select', function () {
        var cityId = $(this).val();
        $('#WorkDistrictId').empty();
        $('#WorkDistrictId').select2({
            ajax: {
                url: '/api/district/ByCity/' + cityId,
                type: 'GET',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        searchQuery: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, params) {
                    var results = [];
                    $.each(data, function (index, item) {
                        results.push({
                            id: item.districtId,
                            text: item.districtName
                        });
                    });

                    return {
                        results: results,
                        pagination: {
                            more: data.length >= 5
                        }
                    };
                },
                cache: true
            },
            placeholder: '-- Chọn Quận, Huyện --',
            minimumInputLength: 0
        });
        $('#WorkWardId').empty(); // Xoá giá trị của trường #WardId khi thay đổi cityId
    });

    $('#WorkDistrictId').on('select2:select', function () {
        var districtId = $(this).val();
        $('#WorkWardId').empty();
        $('#WorkWardId').select2({
            ajax: {
                url: '/api/Ward/ByDistrict/' + districtId,
                type: 'GET',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        searchQuery: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, params) {
                    var results = [];
                    $.each(data, function (index, item) {
                        results.push({
                            id: item.wardId,
                            text: item.wardName
                        });
                    });

                    return {
                        results: results,
                        pagination: {
                            more: data.length >= 5
                        }
                    };
                },
                cache: true
            },
            placeholder: '-- Chọn Xã, Phường --',
            minimumInputLength: 0
        });
    });
});

$(document).ready(function () {
    $('#WorkId').select2({
        ajax: {
            url: '/api/work/',
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.workId,
                        text: item.workName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn nghề nghiệp --',
        minimumInputLength: 0
    });

    $('#EthnicityId').select2({
        ajax: {
            url: '/api/ethnicity/',
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.ethnicityId,
                        text: item.ethnicityName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn dân tộc --',
        minimumInputLength: 0
    });
});

$(document).ready(function () {
    function calculateAge() {
        var birthDateString = $('#BirthDay').val();

        if (birthDateString) {
            var birthDate = new Date(birthDateString);
            var today = new Date();
            var age = today.getFullYear() - birthDate.getFullYear();

            if (
                today.getMonth() < birthDate.getMonth() ||
                (today.getMonth() === birthDate.getMonth() && today.getDate() < birthDate.getDate())
            ) {
                age--;
            }

            $('#Age').val(age);
        } else {
            $('#Age').val('');
        }
    }

    $('#BirthDay').change(function () {
        calculateAge();
    });

    calculateAge();
});


$(document).ready(function () {
    $('#NoIdentityCard').on('change', function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {
            $('#IdentityCard').val('').prop('readonly', true);
            $('#IdentityCard-error').hide();
        } else {
            $('#IdentityCard').prop('readonly', false);
            $('#IdentityCard-error').show();
        }
    });
});

$(document).ready(function () {
    $('#NoPhoneNumber').on('change', function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {
            $('#PhoneNumber').val('').prop('readonly', true);
            $('#PhoneNumber-error').hide();
        } else {
            $('#PhoneNumber').prop('readonly', false);
            $('#PhoneNumber-error').show();
        }
    });
});

$(document).ready(function () {
    $('#search-button').click(function () {
        performSearch();
    });

    $('#search-input').keypress(function (e) {
        if (e.which === 13) {
            performSearch();
        }
    });

    function performSearch() {
        var searchInput = $('#search-input').val();

        $.ajax({
            url: '/Person/Index',
            type: 'GET',
            data: { search: searchInput },
            success: function (result) {
                $('#search-results').html(result);
            },
            error: function () {
                alert('Đã xảy ra lỗi trong quá trình tìm kiếm.');
            }
        });
    }
});

function showImportForm() {
    document.getElementById('importForm').style.display = 'block';
}

function importExcel() {
    var fileInput = document.getElementById('excelFileInput');
    var file = fileInput.files[0];

    var formData = new FormData();
    formData.append('excelFile', file);

    $.ajax({
        url: '/Person/ImportExcel', // Đường dẫn đến endpoint xử lý file Excel trên server
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result.success) {
                alert(result.message);
                window.location.href = '/Person/Index';
            } else {
                alert(result.message);
            }
        },
        error: function (xhr, status, error) {
            alert('Đã xảy ra lỗi khi import file Excel!');
        }
    });
}

$(function () {
    if ($('div.alert.notification').length) {
        setTimeout(() => {
            $('div.alert.notification').fadeOut('slow');
        }, 2500)
    }
});

$(document).ready(function () {
    var cityId = $('#CityId').val();
    $('#DistrictId').select2({
        ajax: {
            url: '/api/district/ByCity/' + cityId,
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.districtId,
                        text: item.districtName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn Quận, Huyện --',
        minimumInputLength: 0
    });
});

$(document).ready(function () {
    var districtId = $('#DistrictId').val();
    $('#WardId').select2({
        ajax: {
            url: '/api/Ward/ByDistrict/' + districtId,
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.wardId,
                        text: item.wardName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn Xã, Phường --',
        minimumInputLength: 0
    });
});

$(document).ready(function () {
    var cityId = $('#WorkCityId').val();
    $('#WorkDistrictId').select2({
        ajax: {
            url: '/api/district/ByCity/' + cityId,
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.districtId,
                        text: item.districtName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn Quận, Huyện --',
        minimumInputLength: 0
    });
});

$(document).ready(function () {
    var districtId = $('#WorkDistrictId').val();
    $('#WorkWardId').select2({
        ajax: {
            url: '/api/Ward/ByDistrict/' + districtId,
            type: 'GET',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchQuery: params.term,
                    page: params.page
                };
            },
            processResults: function (data, params) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.wardId,
                        text: item.wardName
                    });
                });

                return {
                    results: results,
                    pagination: {
                        more: data.length >= 5
                    }
                };
            },
            cache: true
        },
        placeholder: '-- Chọn Xã, Phường --',
        minimumInputLength: 0
    });
});