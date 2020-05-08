echo "After this script, reboot your computer"

sudo dnf -y install dnf-plugins-core

sudo dnf config-manager \
    --add-repo \
    https://download.docker.com/linux/fedora/docker-ce.repo

sudo dnf -y install docker-ce docker-ce-cli containerd.io grubby docker-compose

sudo grubby --update-kernel=ALL --args="systemd.unified_cgroup_hierarchy=0"

sudo systemctl enable docker
sudo systemctl start docker

echo "Reboot Your Computer"

