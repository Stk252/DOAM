# DOAM - Projet de 4e année Bachelier informatique de gestion

## DOAM - Digital Online Asset Management

Permet à l’organisation qui l’utilise de cataloguer et de classifier des ressources numériques en ligne pour 
les mettre ensuite à disposition de visiteurs au travers d’une page web de recherche.

La plateforme supporte différents type de ressources numériques (ou assets) :
• Images de différents formats
• Vidéos
• Streams de radios digitales
• Tracks musicaux
• Podcasts
• …

La plateforme DOAM ne stocke pas physiquement les assets mais conserve un lien (url) vers ces
derniers. Sa grande valeur ajoutée est de permettre d’associer de nombreuses métadonnées aux
médias encodés afin de les caractériser de façon précise. Parmi les métadonnées on trouve :
- Le type de ressource numérique : image, vidéo, …
- Le type physique représenté par le mime type (ex : image/png)
- Des informations générales : titre, description, date de création/publication
- Des informations sur la source, l’auteur et les droits d’utilisation
- Des données sémantiques qui permettent de classifier un asset. Pour une piste musicale, on
peut par exemple identifier le genre musical, l’artiste et l’album. Pour une photo, de
nombreuses classifications sont imaginables.
- Des informations spécifiques au type de média : taille physique, dimensions, durée,
résolution, qualité, framerate, …

Une fois encodées, les métadonnées permettent de fournir des informations précises sur les assets
mais sont également exploitables pour les recherches des utilisateurs.
Voici les fonctionnalités de base de la plateforme :
- Un administrateur de la plateforme, une fois identifié, peut gérer les assets (ajout, édition,
suppression) et gérer les différents types de métadonnées.
- Un visiteur de la plateforme peut effectuer des recherches parmi les assets et obtenir une
liste de résultats. Le système propose un système de recherche libre par mots clés qui peut
être complété par un système de filtres. Une attention particulière est accordée à
l’ergonomie de ces fonctionnalités.
- Pour chaque asset, il est possible de consulter ses détails et d’y accéder en ligne.


