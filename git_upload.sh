#!/bin/bash
ssh -T git@github.com
git add .
git commit -m "$(Date +%Y/%m/%d)"
git push origin master