# EventSourcingConcepts

Pour lancer le projet, il faut construire l'image docker et lancer le container.  
Pour cela, il faut se placer dans le dossier racine du projet et lancer les commandes suivantes dans un terminal Powershell :
        
    docker build -t eventsourcingconcepts -f .\EventSourcingConcepts\Dockerfile .
    docker run -it sha256:a929e2806fd47fbd58d6f1bebb9d607d3ce27d3cfa4335ff35b9e9bf0f193cca (sha de l'image)
    
