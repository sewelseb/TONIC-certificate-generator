# Tonic Certicate Generator
<p align="center">
  <a href="#installation">Installation de l'application</a>
  &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#configuration">Configuration de l'appsettings</a>
</p>

Certificate generator est un générateur de certificat. Il prend en charge un fichier Word faisant office de template, un fichier contact et un fichier d'inventaire afin de générer un PDF en sortie. Certificate generator à été conçu pour fonctionner avec la plateforme mail Sendinblue, il est donc recommandé de configurer l'application avec celui-ci.

## Installation
1. Avoir soit l'exécutable, soit le projet avec son code source
1. Le fichier de configuration "appsettings.json" doit être dans le même dossier que l'exécutable ou dans le dossier du projet 'TonicCertificateGenerator/' (Voir section <a href="#configuration">configuration</a> de l'app.json)
1. Installer la version portable de libreOffice (MultilingualStandard) : https://www.libreoffice.org/download/portable-versions
1. Lancer le programme

## Configuration

Le fichier "appsettings.json" possède un certain nombre de propriétés essentiel au fonctionnement de l'application. Voici son contenu :
```
	"SerilogConf" : Configuration des logs avec l'outils Serilog.
	"MAIL_API_KEY": Il s'agit de la clé d'API de la plateforme mail,
	"SMTP_SERVER": Serveur SMTP de la plateforme,
	"SMTP_PORT": Port SMTP supporté (587),
	"SMTP_LOGIN": Login de la plateforme mail,
	"SMTP_PASSWORD": Mot de passe de la plateforme mail,
	"SMTP_TEMPLATE_ID": L'ID du corps de mail sera récupéré et envoyé avec le certificat généré ,
	"OUTPUT_DIR": La sortie des fichiers générés,
	"TEMPLATE_PATH": L'emplacement exacte du modèle de fichier word (nom+extension),
	"SOURCE_PATH": L'emplacement exacte du fichier de contact (nom+extension),
	"INVENTORY_PATH": L'emplacement exacte du fichier d'inventaire (nom+extension),
	"LiBREOFFICE_EXEC_PATH": L'emplacement de l'exécutable de libre office portable (LibreOfficePortable/App/libreoffice/program/soffice.exe),
	"KEYWORD_REPLACED_NAME": Keyword qui sera remplacé dans le template par le nom,
	"KEYWORD_REPLACED_SERIAL": "Keyword qui sera remplacé dans le template par le numéro de série",
	"KEYWORD_REPLACED_CONFNAME": "Keyword qui sera remplacé dans le template par le nom de la conférence",
	"KEYWORD_REPLACED_CONFDATE": "Keyword qui sera remplacé dans le template par la date de la conférence",
	"CONFERENCE_NAME": Nom de la conférence. Celui-ci remplacera le keyword de "KEYWORD_REPLACED_CONFNAME",
	"CONFERENCE_DATE": Date de la conférence. Celui-ci remplacera le keyword de "KEYWORD_REPLACED_CONFDATE"
```

Un exemple de configuration de <a href="https://serilog.net/">Serilog</a> avec les propriétés suivantes dans "appsettings.json" :
```
	"SerilogConf": {
		"Using": [ "Serilog.Filters.Expressions" ],
		"WriteTo": [
			{ "Name": "Console" },
			{
				"Name": "File",
				"Args": {
					"path": "logs2.txt"
				}
			},
			{
				"Name": "Logger",
				"Args": {
					"configureLogger": {
						"Filter": [
							{
								"Name": "ByIncludingOnly",
								"Args": {
									"expression": "@Level = 'Error'"
								}
							}
						],
						"WriteTo": [
							{
								"Name": "File",
								"Args": {
									"path": "logsError.txt"
								}
							}
						]
					}
				}
			}
		]
	},

```