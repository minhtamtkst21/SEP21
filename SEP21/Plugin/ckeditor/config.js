/// <reference path="config.js" />
/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
    config.filebrowserBrowseUrl = '/SEP24Team5/Plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/SEP24Team5/Plugin/lib/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/SEP24Team5/Plugin/lib/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/SEP24Team5/Plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data/';
    config.filebrowserFlashUploadUrl = '/SEP24Team5/Plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    CKFinder.setupCKEditor(null, '/SEP24Team5/Plugin/ckfinder/');
	// The toolbar groups arrangement, optimized for two toolbar rows.
    config.toolbarGroups = [
        { name: 'document', groups: ['mode', 'document', 'doctools'] },
        { name: 'clipboard', groups: ['clipboard', 'undo'] },
        { name: 'editing', groups: ['find', 'selection'] },
        { name: 'links' },
        { name: 'insert' },
        { name: 'forms' },
        { name: 'tools' },
        { name: 'others' },
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'paragraph', groups: ['list'] },
        { name: 'styles' }
    ];

	// Remove some buttons provided by the standard plugins, which are
	// not needed in the Standard(s) toolbar.
	config.removeButtons = 'Anchor'//'Subscript,Superscript';

	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3;pre';

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';

    //cho phép viết các thuộc tính của thẻ 
	config.allowedContent = true;

    //Làm rỗng editor
	config.enterMode = CKEDITOR.ENTER_BR;

    //Chiều cao của editor
	config.height = 350;

    //Cho phép sử dung UTF-8
	config.defaultLanguage = 'vi';
};
