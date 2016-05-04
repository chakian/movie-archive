module.exports = function(grunt) {
	grunt.loadNpmTasks('grunt-angular-gettext');
	
	// Project configuration.
	grunt.initConfig({
		pkg: grunt.file.readJSON('package.json'),
		nggettext_extract: {
			pot: {
				files: {
					'po/template.pot': ['**/*.html']
				}
			}
		},
		nggettext_compile: {
			all: {
				files: {
					'app/translations.js': ['po/*.po']
				}
			}
		}
	});
};