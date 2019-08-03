FROM mongo

COPY *.json /

#CMD mongorestore --host skills --db SkillsDb /collections
CMD mongoimport --host skills --db SkillsDb --collection skills --type json --file /skills.json --jsonArray

#CMD mongoimport --host skills --db SkillsDb --collection projects --mode insert --type json --file /projects.json --jsonArray
#CMD mongoimport --host skills --db SkillsDb --collection projects --mode upsert --type json --file /projects.json --jsonArray
#CMD mongoimport --host skills --db SkillsDb --collection projects --mode merge --type json --file /projects.json --jsonArray

#CMD ls -1 *.json | while read jsonfile; do mongoimport --host skills --db SkillsDb -d support -c logs -file $jsonfile --jsonArray -type json; done

#CMD mongoimport --host skills --db SkillsDb --collection projects --type json --file /projects.json --jsonArray
