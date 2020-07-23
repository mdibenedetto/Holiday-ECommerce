AZURE_REPO=https://dreamholiday.scm.azurewebsites.net:443/dreamholiday.git
USER=$dreamholiday
PASSWORD=uKmh2aN6fFnhSuDCxz22pgLtlBMvBk084QjdkqhJLu0GmHeuTXTCgntNpudj

 

# git add .
git remote add azure $AZURE_REPO

git push azure master
